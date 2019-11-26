using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebBetApp.Main;
using WebBetApp.Model.Database;
using WebBetApp.Model.ViewModels;

namespace WebBetApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IWebBetQueries _webBetQueries;

        public UserProfileController(IWebBetQueries webBetQueries)
        {
            _webBetQueries = webBetQueries;
        }

        [HttpGet]
        public async Task<object> GetUserProfileDetails()
        {
            return  _webBetQueries.GetUserDetails(User.Claims.First(cl => cl.Type == "UserId").Value);
        }
    }
}