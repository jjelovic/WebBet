using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebBetApp.Main;
using WebBetApp.Model;
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
        private readonly AppSettings _appSettings;
        private readonly IWebBetQueries _webBetQueries;

        public ApplicationUserController( 
              UserManager<ApplicationUser> userManager,
              SignInManager<ApplicationUser> signInManager,
              IOptions<AppSettings> appSettings,
              IWebBetQueries webBetQueries
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appSettings = appSettings.Value;
            _webBetQueries = webBetQueries;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> PostApplicationUser(UserRegistration user)
        {
            var result = await _webBetQueries.CreateUser(user);

            if (result.Succeeded)
                return Ok(result);
            else
                return BadRequest( new { message = "User is not created"});
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(Login loginModel)
        {
            var user = await _userManager.FindByNameAsync(loginModel.UserName);

            if (user != null && await _userManager.CheckPasswordAsync(user, loginModel.Password))
            {
                var tokenDesc = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("UserId", user.Id.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JsonWebTokenSecurityKey)), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDesc);
                var token = tokenHandler.WriteToken(securityToken);

                return Ok(new { token });
            }

            else
                return BadRequest(new { message = "Username or password are incorect" });
        }
    }
}