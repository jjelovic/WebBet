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

        public ApplicationUserController( 
              UserManager<ApplicationUser> userManager,
              SignInManager<ApplicationUser> signInManager,
              IOptions<AppSettings> appSettings
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appSettings = appSettings.Value;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<Object> PostApplicationUser(UserRegistration user)
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

                return Ok( new { token } );
            } 

            else
                return BadRequest(new { message = "Username or password are incorect" });
        }
    }
}