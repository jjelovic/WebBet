using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebBetApp.Model.Database.DatabaseModel
{
    public class Sport
    {
        public Sport()
        {
            Matches = new HashSet<Match>();
        }

        [Key]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string Name { get; set; }

        public virtual ICollection<Match> Matches { get; set; }
    }
}
