using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBetApp.Model.Database;

namespace WebBetApp.Model.ViewModels
{
    public class WebWallet
    {
        public double Amount { get; set; }
        public ApplicationUser User { get; set; }
        public double WalletBalance { get; set; }
    }
}
