using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebBetApp.Model.Database.DatabaseModel
{
    public class TicketMatch
    {
        [Key]
        public int TicketMatchId { get; set; }
        [Column(TypeName = "nvarchar(max)")]
        public string Pair { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string Type { get; set; }

        [Column(TypeName = "decimal(19,2)")]
        public double Quota { get; set; }

        public Nullable<int> TicketId { get; set; }
        public virtual Ticket Ticket { get; set; }
    }
}
