using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBetApp.Model.Database.DatabaseModel;
using WebBetApp.Model.ViewModels;

namespace WebBetApp.Main
{
    public interface IWebBetQueries
    {
        IEnumerable<WebMatchOffer> GetMatchesGroupedBySport();

        void MakeTransaction(WebWallet webWalletDeposit);

        IEnumerable<WebTicket> GetAllTickets();

        WebWallet GetBalance();

        void PostWebTicketToDb(WebTicket webTicket);

        void DeleteTicketFromDb(string ticketCode);
    }
}
