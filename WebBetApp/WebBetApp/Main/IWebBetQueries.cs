using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBetApp.Model.Database;
using WebBetApp.Model.Database.DatabaseModel;
using WebBetApp.Model.ViewModels;

namespace WebBetApp.Main
{
    public interface IWebBetQueries
    {
        IEnumerable<WebMatchOffer> GetMatchesGroupedBySport();

        void MakeTransaction(WebWallet webWalletDeposit, ApplicationUser user);

        IEnumerable<WebTicket> GetAllTickets(ApplicationUser user);

        void PostWebTicketToDb(WebTicket webTicket, ApplicationUser user);

        void DeleteTicketFromDb(string ticketCode);

        WebWallet GetUserWalletBalance(ApplicationUser user);


    }
}
