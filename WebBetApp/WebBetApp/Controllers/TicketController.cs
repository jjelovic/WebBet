using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBetApp.Main;
using WebBetApp.Model.Database;
using WebBetApp.Model.Database.DatabaseModel;
using WebBetApp.Model.ViewModels;

namespace WebBetApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly IWebBetQueries webBetQueries;

        public TicketController(IWebBetQueries webBetQueries)
        {
            this.webBetQueries = webBetQueries;
        }

        // GET: api/Ticket
        [HttpGet]
        public IEnumerable<WebTicket> GetTickets()
        {
            return webBetQueries.GetAllTickets();
        }

        [HttpPost]
        public void PostTicket(WebTicket webTicket)
        {
            webBetQueries.PostWebTicketToDb(webTicket);
        }

        [HttpDelete("{ticketCode}")]
        public void DeleteTicket(string ticketCode)
        {
            webBetQueries.DeleteTicketFromDb(ticketCode);
        }
    }
}