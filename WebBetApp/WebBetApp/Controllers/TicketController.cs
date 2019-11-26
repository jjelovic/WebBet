using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebBetApp.Main;
using WebBetApp.Model.ViewModels;

namespace WebBetApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly IWebBetQueries _webBetQueries;

        public TicketController(IWebBetQueries webBetQueries)
        {
            _webBetQueries = webBetQueries;
        }

        // GET: api/Ticket/userId
        [HttpGet("{userId}")]
        public IEnumerable<WebTicket> GetTickets(string userId)
        {
            return _webBetQueries.GetAllTickets(userId);
        }

        [HttpPost("{userId}")]
        [Authorize]
        public IActionResult PostTicket(WebTicket webTicket, string userId)
        {
            var response = _webBetQueries.PostWebTicketToDb(webTicket, userId);

            if (response.Count == 0)
                return Ok();
            else
                return BadRequest(new { message = response });
        }

        [HttpDelete("{ticketId}")]
        public IActionResult DeleteTicket(int ticketId)
        {
            var result = _webBetQueries.DeleteTicketFromDb(ticketId);

            if (result != null)
                return Ok(new { message = "Ticket deleted successfully"} );
            else
                return BadRequest(new { message = "Ticket was not found"} );
            
        }
    }
}