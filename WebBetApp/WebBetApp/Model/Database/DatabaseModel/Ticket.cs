using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebBetApp.Model.Database.DatabaseModel
{
    public class Ticket
    {
        public Ticket()
        {
            this.TicketMatches = new HashSet<TicketMatch>();
        }

        [Key]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string TicketCode { get; set; }

        [Column(TypeName = "decimal(19,2)")]
        public double Stake { get; set; }

        [Column(TypeName = "decimal(19,2)")]
        public double PossibleReturn { get; set; }

        [Column(TypeName = "decimal(19,2)")]
        public double StakeWithManipulationCosts { get; set; }

        [Column(TypeName = "decimal(19,2)")]
        public double TotalMatchesCoefficient { get; set; }

        [Column(TypeName ="nvarchar(450)")]
        public string ApplicationUserId { get; set; }
    
        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<TicketMatch> TicketMatches{ get; set; }

    }
}
