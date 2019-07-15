using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBetApp.Model.Database;
using WebBetApp.Model.Database.DatabaseModel;
using WebBetApp.Model.ViewModels;
using WebBetApp.Helper;
using WebBetApp.Main.Validation;


namespace WebBetApp.Main
{
    public class WebBetQuriesImpl : IWebBetQueries
    {
        private readonly WebBetDbContext context;

        public WebBetQuriesImpl( WebBetDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<WebMatchOffer> GetMatchesGroupedBySport()
        {
            return context.Sports.SelectMany(m => m.Matches)
                                 .GroupBy(m => m.Sport)
                                 .ToList()
                                 .Select(res =>
                                             new WebMatchOffer
                                             {
                                                 Sport = res.Key.Name,
                                                 Matches = res.ToList()
                                             }
                                         )
                                 .ToList();
        }

        public IEnumerable<WebTicket> GetAllTickets()
        {
            return context.Tickets.Include(i => i.TicketMatches)
                                  .Select(t =>
                                           new WebTicket
                                           {
                                               Id = t.Id,
                                               TicketCode = t.TicketCode,
                                               Stake = t.Stake,
                                               PossibleReturn = t.PossibleReturn,
                                               StakeWithManipulationCosts = t.StakeWithManipulationCosts,
                                               TotalMatchesCoefficient = t.TotalMatchesCoefficient,
                                               TicketMatches = t.TicketMatches
                                           }).ToList();
        }

        public void PostWebTicketToDb(WebTicket webTicket)
        {
            var balance = GetBalance();

            WalletValidation.ValidateWalletBalanceGreaterThenStake(webTicket, balance);
            TicketValidation.ValidateTicket(webTicket, context);

            try
            {

                var ticket = new Ticket
                {
                    TicketCode = Service.GenerateTicketCode(),
                    Stake = webTicket.Stake,
                    PossibleReturn = webTicket.PossibleReturn,
                    StakeWithManipulationCosts = webTicket.StakeWithManipulationCosts,
                    TicketMatches = webTicket.TicketMatches.ToList(),
                    TotalMatchesCoefficient = webTicket.TotalMatchesCoefficient
                };

                context.Tickets.Add(ticket);

                foreach (var match in webTicket.TicketMatches)
                {
                   context.TicketMatches.Add(match);
                }

                context.SaveChanges();

                var withdrawTransaction = new WebWallet
                {
                    Amount = -webTicket.Stake,

                };

                MakeTransaction(withdrawTransaction);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteTicketFromDb(string ticketCode)
        {
            var ticket = context.Tickets.Include(tm => tm.TicketMatches)
                                        .SingleOrDefault(t => t.TicketCode == ticketCode);

            foreach(var ticketMatch in ticket.TicketMatches)
            {
                context.TicketMatches.Remove(ticketMatch);
            }

            context.Tickets.Remove(ticket);

            context.SaveChanges();
        } 

        public WebWallet GetBalance()
        {
            var balance = context.Wallet.Select(w => w.Balance).Sum();

            return new WebWallet { Amount = balance };
        }

        public void MakeTransaction(WebWallet webWalletDeposit)
        {
            var balance = GetBalance();
            WalletValidation.ValidateBalanceMustNotBeLessThanZero(webWalletDeposit, balance);

            try
            {
                var transaction = new Transaction
                {
                   Amount = webWalletDeposit.Amount,
                   TransactionDate = DateTime.Now
                };

                context.Transactions.Add(transaction);

                context.SaveChanges();
           
                UpdateBalance();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void UpdateBalance()
        {
            var wallet = context.Wallet.FirstOrDefault();

            wallet.Balance = context.Transactions.Sum(tr => tr.Amount);

            context.SaveChanges();
        }
    }
}
