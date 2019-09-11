using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebBetApp.Model.Database;
using WebBetApp.Model.ViewModels;

namespace WebBetApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        public ApplicationUserController( UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<Object> PostApplicationUser(User user)
        {
            var appUser = new ApplicationUser()
            {
                UserName = user.UserName,
                FullName = user.FullName,
                Email = user.Email,
            };
            try
            {
                var result = await _userManager.CreateAsync(appUser, user.Password);

                return Ok(result);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}