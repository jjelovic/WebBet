using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebBetApp.Model.Database.DatabaseModel;
using WebBetApp.Model.ViewModels;

namespace WebBetApp.Main
{
    public interface IWebBetQueries
    {
        IEnumerable<WebMatchOffer> GetMatchesGroupedBySport();

        void MakeTransaction(WebWallet webWalletDeposit, string userId);

        IEnumerable<WebTicket> GetAllTickets(string userId);

        List<string> PostWebTicketToDb(WebTicket webTicket, string userId);

        Ticket DeleteTicketFromDb(int ticketId);

        WebWallet GetUserWalletBalance(string userId);

        UserDetails GetUserDetails(string userId);

        Task<IdentityResult> CreateUser(UserRegistration userRegistration);
    }
}
