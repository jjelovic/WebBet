using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebBetApp.Main;
using WebBetApp.Model.Database;
using WebBetApp.Model.ViewModels;

namespace WebBetApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly IWebBetQueries _webBetQueries;

        public WalletController(IWebBetQueries webBetQueries)
        {
            _webBetQueries = webBetQueries;
        }

        [HttpGet("{userId}")]
        public WebWallet GetWalletBalance(string userId)
        {
            return _webBetQueries.GetUserWalletBalance(userId);
        }

        [HttpPost("{userId}")]
        [Authorize]
        public IActionResult PostDepositToWallet(WebWallet webWalletDeposit, string userId)
        {
            _webBetQueries.MakeTransaction(webWalletDeposit, userId);

            return Ok();
        }
    }
}