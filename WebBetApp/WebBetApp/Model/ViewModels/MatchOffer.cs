using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBetApp.Model.Database.DatabaseModel;

namespace WebBetApp.Model.ViewModels
{
    public class MatchOffer
    {
        public string Sport { get; set; }
        public IEnumerable<Match> Matches { get; set; }
    }
}
