using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebBetApp.Model.Database;
using WebBetApp.Model.Database.DatabaseModel;
using WebBetApp.Model.ViewModels;
using WebBetApp.Helper;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace WebBetApp.Main
{
    public class WebBetQuriesImpl : IWebBetQueries
    {
        private readonly WebBetDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        private readonly IValidator<WebTicket> _ticketValidator;

        public WebBetQuriesImpl
            (
                WebBetDbContext context,
                UserManager<ApplicationUser> userManager,
                IValidator<WebTicket> ticketValidator
            )
        {
            _context = context;
            _userManager = userManager;
            _ticketValidator = ticketValidator;
        }

        public IEnumerable<WebMatchOffer> GetMatchesGroupedBySport()
        {
            return _context.Sports
                .SelectMany(m => m.Matches)
                .GroupBy(m => m.Sport)
                .ToList()
                .Select(res =>
                              new WebMatchOffer
                              {
                                 Sport = res.Key.Name,
                                 Matches = res.ToList()
                              })
                .ToList();
        }

        public IEnumerable<WebTicket> GetAllTickets(string userId)
        {
            return _context.Tickets
                .Include(i => i.TicketMatches)
                .Where( tm => tm.ApplicationUserId == userId)
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

        public List<string> PostWebTicketToDb(WebTicket webTicket, string userId)
        {
            var response = new List<string>();

            webTicket.UserBalance =  GetUserWalletBalance(userId).Amount;
            webTicket.User = _context.ApplicationUsers.FirstOrDefault(x => x.Id == userId);
          
            var result = _ticketValidator.Validate(webTicket);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    response.Add(error.ToString());
                }
                return response;
            }
            else
            {
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
                        ApplicationUserId = userId
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

                    MakeTransaction(withdrawTransaction, userId);

                    return response;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public Ticket DeleteTicketFromDb(int ticketId)
        {
            try
            {
                var ticket = _context.Tickets
                                .Include(tm => tm.TicketMatches)
                                .SingleOrDefault(t => t.Id == ticketId);

                if (ticket != null)
                {
                    foreach (var ticketMatch in ticket.TicketMatches)
                    {
                        _context.TicketMatches.Remove(ticketMatch);
                    }

                    _context.Tickets.Remove(ticket);

                    _context.SaveChanges();

                    return ticket;
                }
                else
                    return null;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        } 

        public void MakeTransaction(WebWallet webWalletDeposit, string userId)
        {
            try
            {
               var transaction = new Transaction
               {
                  Amount = webWalletDeposit.Amount,
                  TransactionDate = DateTime.Now,
                  ApplicationUserId = userId
               };

               _context.Transactions.Add(transaction);

               _context.SaveChanges();

            }
            catch(Exception ex)
            {
               throw ex;
            }
            
        }

        public WebWallet GetUserWalletBalance(string userId)
        {
            var balanceAmount =  _context.Transactions
                    .Where(t => t.ApplicationUserId == userId)
                    .Sum(a => a.Amount);

            return new WebWallet
            {
                Amount = balanceAmount
            };
        }

        public UserDetails GetUserDetails(string userId)
        {
            return  _context.ApplicationUsers
                .Where(x => x.Id == userId)
                .Select(user =>
                         new UserDetails
                         {
                             UserId = userId,
                             FullName = user.FullName,
                             UserName = user.UserName,
                             UserWalletBalance = GetUserWalletBalance(userId).Amount
                         }
                       )
                .SingleOrDefault();
        }

        public async Task<IdentityResult> CreateUser(UserRegistration userRegistration)
        {
            var applicationUser = new ApplicationUser
            {
                UserName = userRegistration.UserName,
                FullName = userRegistration.FullName,
                Email = userRegistration.Email
            };

            var result = await _userManager.CreateAsync(applicationUser, userRegistration.Password);

            return result ;
        }
    }
}
