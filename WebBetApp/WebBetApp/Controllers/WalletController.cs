using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebBetApp.Main;
using WebBetApp.Model.ViewModels;

namespace WebBetApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly IWebBetQueries webBetQueries;
        public WalletController(IWebBetQueries webBetQueries)
        {
            this.webBetQueries = webBetQueries;
        }

        [HttpGet]
        public WebWallet GetWalletBalance()
        {
            return webBetQueries.GetBalance();
        }

        [HttpPost]
        public void PostDepositToWallet(WebWallet webWalletDeposit)
        {
            webBetQueries.PostDepositTransaction(webWalletDeposit);
        }
    }
}