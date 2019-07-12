using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebBetApp.Model.ViewModels;

namespace WebBetApp.Main.Validation
{
    public abstract class Validation<TypeOfObjectToValidate>
    {
        public string  ErrorMessage { get; set; }
        public bool IsValid { get; set; }
        public abstract void Validate(TypeOfObjectToValidate objectToValidate);
    }


    public class WalletValidation : Validation<WebTicket>
    {

        public WalletValidation(IWebBetQueries webBetQueries)
        {
            this.webBetQueries = webBetQueries;
            this.ErrorMessage = "Stake cannot be greater than wallet balance. Insufficient funds in wallet.";
        }

 
        private readonly IWebBetQueries webBetQueries;
         

        public override void Validate(WebTicket objectToValidate)
        {
            var balance = webBetQueries.GetBalance();

            this.IsValid = !(balance.Amount < objectToValidate.Stake);

            if (!this.IsValid) throw new ValidationException(this.ErrorMessage);

        }
    }
}
