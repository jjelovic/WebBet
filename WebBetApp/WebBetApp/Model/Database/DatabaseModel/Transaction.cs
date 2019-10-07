using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebBetApp.Model.Database.DatabaseModel
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "decimal(19,2)")]
        public double Amount { get; set; }

        [Column(TypeName = "nvarchar(450)")]
        public string ApplicationUserId { get; set; }

        [Column(TypeName = "datetimeoffset(7)")]
        public DateTimeOffset TransactionDate { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
