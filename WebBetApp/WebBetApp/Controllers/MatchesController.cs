using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
    public class MatchesController : ControllerBase
    {
        private readonly IWebBetQueries webBetQuries;

        public MatchesController(IWebBetQueries webBetQuries)
        {
            this.webBetQuries = webBetQuries;
        }

        // GET: api/Matches
        [HttpGet]
        public IEnumerable<MatchOffer> GetMatchesForWebOffer()
        {
            return webBetQuries.GetMatchesGroupedBySport();
        }
    }
}