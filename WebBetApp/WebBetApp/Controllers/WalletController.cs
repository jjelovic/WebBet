using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        private UserManager<ApplicationUser> _userManager;

        public WalletController(IWebBetQueries webBetQueries, UserManager<ApplicationUser> userManager)
        {
            _webBetQueries = webBetQueries;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<WebWallet> GetWalletBalance()
        {
            var user = await _userManager.FindByIdAsync(User.Claims.First(cl => cl.Type == "UserId").Value);

            return _webBetQueries.GetUserWalletBalance(user);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostDepositToWallet(WebWallet webWalletDeposit)
        {
            var user =  await _userManager.FindByIdAsync(User.Claims.First(cl => cl.Type == "UserId").Value);

            _webBetQueries.MakeTransaction(webWalletDeposit, user);

            return Ok();
        }
    }
}