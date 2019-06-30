using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebBetApp.Model.Database.DatabaseModel
{
    public class Match
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string Pair { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public double? Type1 { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public double? TypeX { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public double? Type2 { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public double? Type1X { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public double? TypeX2 { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public double? Type12 { get; set; }

        public bool IsPartOfTopOffer { get; set; }
        public int SportId { get; set; }

        public virtual Sport Sport { get; set; }
    }
}
