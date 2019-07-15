using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebBetApp.Helper;
using WebBetApp.Model.Database;
using WebBetApp.Model.ViewModels;

namespace WebBetApp.Main.Validation
{

    public class TicketValidation
    {
        public static bool IsValidTicket { get; set; }
        public static bool AreValidTicketMatches { get; set; }

        public static void ValidateTicket(WebTicket webTicket, WebBetDbContext context)
        {
            IsValidTicket = (webTicket.PossibleReturn > 0 &&
                             webTicket.Stake > 0 &&
                             webTicket.StakeWithManipulationCosts > 0 && 
                             webTicket.TotalMatchesCoefficient > 0 && 
                             webTicket.TicketMatches.Count() > 0);

            if(!IsValidTicket) throw new ValidationException("Invalid ticket inputs.");

            var topOfferMatches = webTicket.TicketMatches.Where(m => m.SelectedInTopOffer).ToList();

            if (topOfferMatches.Count == 1 && webTicket.TicketMatches.Count() < 6) throw new ValidationException("Top offer match must be in combination with 5 regular offer matches.");
            
            if (topOfferMatches.Count > 1) throw new ValidationException("Top offer matches cannot combine each other. Only one allowed.");
            
            foreach (var match in webTicket.TicketMatches)
            {
                AreValidTicketMatches = (!Check.IsNullOrEmptyOrWhitespace(match.Pair) &&
                                          Check.IsValidMatchType(match.Type) &&
                                          match.Quota > 1.1 ) ;

                var matchExistInOffer = context.Matches.Any(m => m.Id == match.MatchId);

                if(!AreValidTicketMatches && !matchExistInOffer) throw new ValidationException("Ticket matches have invalid parameters.");
            }
        }
    }

    public class WalletValidation 
    {
        public static bool IsValid { get; set; }

        public static void ValidateWalletBalanceGreaterThenStake(WebTicket objectToValidate, WebWallet walletBalance)
        {
            IsValid = (walletBalance.Amount >= objectToValidate.Stake);

            if (!IsValid) throw new ValidationException("Stake cannot be greater than wallet balance. Insufficient funds.");
        }

        public static void ValidateBalanceMustNotBeLessThanZero(WebWallet walletDeposit, WebWallet walletBalance)
        {
            IsValid = !(walletBalance.Amount <= 0 &&  walletDeposit.Amount < 0);

            if (!IsValid) throw new ValidationException("Invalid deposit, wallet amuont cannot be less than zero.");
        }

    }
}
