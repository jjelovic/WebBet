using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBetApp.Model.Database.DatabaseModel;

namespace WebBetApp.Model.ViewModels
{
    public class WebTicket
    {
        public int Id { get; set; }

        public string TicketCode { get; set; }
        
        public double Stake { get; set; }

        public double PossibleReturn { get; set; }
   
        public double StakeWithManipulationCosts { get; set; }

        public double TotalMatchesCoefficient { get; set; }
        public IEnumerable<TicketMatch> TicketMatches { get; set; }
    }
}
