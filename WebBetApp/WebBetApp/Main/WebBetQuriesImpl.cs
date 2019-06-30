using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBetApp.Model.Database;
using WebBetApp.Model.Database.DatabaseModel;
using WebBetApp.Model.ViewModels;

namespace WebBetApp.Main
{
    public class WebBetQuriesImpl : IWebBetQueries
    {
        private readonly WebBetDbContext context;

        public WebBetQuriesImpl(WebBetDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<MatchOffer> GetMatchesGroupedBySport()
        {
            return context.Sports.SelectMany(m => m.Matches)
                                 .GroupBy(m => m.Sport)
                                 .ToList()
                                 .Select(res =>
                                               new MatchOffer
                                               {
                                                   Sport = res.Key.Name,
                                                   Matches = res.ToList()
                                               }
                                         )
                                 .ToList();
        }

        public WebWallet GetBalance()
        {
            var balance = context.Wallet.Select(w => w.Balance).Sum();

            return new WebWallet { Amount = balance };
        }

        public void PostDepositTransaction(WebWallet webWalletDeposit)
        {
            var transaction = new Transaction
                             {
                                Amount = webWalletDeposit.Amount,
                                TransactionDate = DateTime.Now
                             };

            context.Transactions.Add(transaction);

            context.SaveChanges();
           
            UpdateBalance();
        }


        private void UpdateBalance()
        {
            var wallet = context.Wallet.FirstOrDefault();

            wallet.Balance = context.Transactions.Sum(tr => tr.Amount);

            context.SaveChanges();
        }
    }
}
