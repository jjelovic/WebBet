using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        private readonly IWebBetQueries _webBetQueries;
        private UserManager<ApplicationUser> _userManager;

        public TicketController(IWebBetQueries webBetQueries, UserManager<ApplicationUser> userManager)
        {
            _webBetQueries = webBetQueries;
            _userManager = userManager;
        }

        // GET: api/Ticket
        [HttpGet]
        public async Task<IEnumerable<WebTicket>> GetTickets()
        {
            var user = await _userManager.FindByIdAsync(User.Claims.First(cl => cl.Type == "UserId").Value);

            return _webBetQueries.GetAllTickets(user);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostTicket(WebTicket webTicket)
        {
            var user = await _userManager.FindByIdAsync(User.Claims.First(cl => cl.Type == "UserId").Value); 

            _webBetQueries.PostWebTicketToDb(webTicket, user);

            return Ok();
        }

        [HttpDelete("{ticketCode}")]
        public void DeleteTicket(string ticketCode)
        {
            _webBetQueries.DeleteTicketFromDb(ticketCode);
        }
    }
}