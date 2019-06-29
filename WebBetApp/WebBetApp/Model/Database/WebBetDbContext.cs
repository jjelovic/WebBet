using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBetApp.Model.Database.DatabaseModel;

namespace WebBetApp.Model.Database
{
    public class WebBetDbContext: DbContext
    {
        public WebBetDbContext( DbContextOptions<WebBetDbContext> options)
            :base(options)
        {
            
        }

        public DbSet<Match> Matches { get; set; }
        public DbSet<Sport> Sports { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Wallet> Wallet { get; set; }
    }
}
