using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBetApp.Model.Database;
using WebBetApp.Model.ViewModels;

namespace WebBetApp.Main
{
    public class WebBetQuriesImpl : IWebBetQueries
    {
        private readonly WebBetDbContext context;

        public WebBetQuriesImpl(WebBetDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<MatchOffer> GetMatchesGroupedBySport()
        {
            return context.Sports
                                 .SelectMany(m => m.Matches)
                                 .GroupBy(m => m.Sport)
                                 .ToList()
                                 .Select(res =>
                                               new MatchOffer
                                               {
                                                   Sport = res.Key.Name,
                                                   Matches = res.ToList()
                                               }
                                         )
                                 .ToList();
        }
    }
}
