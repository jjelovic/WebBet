using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBetApp.Model.ViewModels;

namespace WebBetApp.Main
{
    public interface IWebBetQueries
    {
        IEnumerable<MatchOffer> GetMatchesGroupedBySport();
    }
}
