using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebBetApp.Model.Database.DatabaseModel;

namespace WebBetApp.Model.Database
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.Tickets = new HashSet<Ticket>();
            this.Transactions = new HashSet<Transaction>();
        }

        [Column(TypeName = "nvarchar(max)")]
        public string FullName { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
