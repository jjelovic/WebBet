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
        private readonly WebBetDbContext _context;

        public WebBetQuriesImpl( WebBetDbContext context)
        {
            _context = context;
        }

        public IEnumerable<WebMatchOffer> GetMatchesGroupedBySport()
        {
            return _context.Sports.SelectMany(m => m.Matches)
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

        public IEnumerable<WebTicket> GetAllTickets(ApplicationUser user)
        {
            return 
                _context.Tickets.Include(i => i.TicketMatches)
                                .Where( tm => tm.ApplicationUserId == user.Id)
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
                                           })
                               .ToList();
        }

        public void PostWebTicketToDb(WebTicket webTicket, ApplicationUser user)
        {
            using (_context)
            {
                var balance = GetUserWalletBalance(user).Amount;

                WalletValidation.ValidateWalletBalanceGreaterThenStake(webTicket, balance);
                TicketValidation.ValidateTicket(webTicket, _context);

                try
                {
                    var ticket = new Ticket
                    {
                        TicketCode = Service.GenerateTicketCode(),
                        Stake = webTicket.Stake,
                        PossibleReturn = webTicket.PossibleReturn,
                        StakeWithManipulationCosts = webTicket.StakeWithManipulationCosts,
                        TicketMatches = webTicket.TicketMatches.ToList(),
                        TotalMatchesCoefficient = webTicket.TotalMatchesCoefficient,
                        ApplicationUserId  = user.Id
                    };

                    _context.Tickets.Add(ticket);

                    foreach (var match in webTicket.TicketMatches)
                    {
                       _context.TicketMatches.Add(match);
                    }

                    _context.SaveChanges();

                    var withdrawTransaction = new WebWallet
                    {
                        Amount = -webTicket.Stake,
                    };

                    MakeTransaction(withdrawTransaction, user);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void DeleteTicketFromDb(string ticketCode)
        {
            var ticket = _context.Tickets.Include(tm => tm.TicketMatches).SingleOrDefault(t => t.TicketCode == ticketCode);

            foreach(var ticketMatch in ticket.TicketMatches)
            {
                _context.TicketMatches.Remove(ticketMatch);
            }

            _context.Tickets.Remove(ticket);

            _context.SaveChanges();
        } 

        public void MakeTransaction(WebWallet webWalletDeposit, ApplicationUser user)
        {
            var balance = GetUserWalletBalance(user).Amount;
            WalletValidation.ValidateBalanceMustNotBeLessThanZero(webWalletDeposit, balance);

            try
            {
               var transaction = new Transaction
               {
                  Amount = webWalletDeposit.Amount,
                  TransactionDate = DateTime.Now,
                  ApplicationUserId = user.Id
               };

               _context.Transactions.Add(transaction);

               _context.SaveChanges();
            }
            catch(Exception ex)
            {
               throw ex;
            }
            
        }
        public WebWallet GetUserWalletBalance(ApplicationUser user)
        {
            var balanceAmount =  _context.Transactions
                    .Where(t => t.ApplicationUserId == user.Id)
                    .Sum(a => a.Amount);

            return new WebWallet
            {
                Amount = balanceAmount
            };
        }
    }
}
