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
using WebBetApp.Model.ViewModels;

namespace WebBetApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;
        private readonly WebBetDbContext _context;
        private readonly IWebBetQueries _webBetQueries;

        public UserProfileController(UserManager<ApplicationUser> userManager, WebBetDbContext context, IWebBetQueries webBetQueries)
        {
            _userManager = userManager;
            _context = context;
            _webBetQueries = webBetQueries;
        }

        [HttpGet]
        [Authorize]
        public async Task<object> GetUserProfileDetails()
        {
            var user = await _userManager.FindByIdAsync(User.Claims.First(cl => cl.Type == "UserId").Value);

            var userBalance = _webBetQueries.GetUserWalletBalance(user);

            return new UserDetails
            {
                UserName = user.UserName,
                FullName = user.FullName,
                UserWalletBalance = userBalance.Amount
            };
        }
    }
}