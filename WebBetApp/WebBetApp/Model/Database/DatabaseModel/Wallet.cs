using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebBetApp.Model.Database.DatabaseModel
{
    public class Wallet
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "decimal(19,2)")]
        public double Balance { get; set; }
    }
}
