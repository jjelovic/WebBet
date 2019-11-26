using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBetApp.Helper;
using WebBetApp.Model.Database;
using WebBetApp.Model.Database.DatabaseModel;
using WebBetApp.Model.ViewModels;

namespace WebBetApp.Main.Validation
{
    public class TicketValidator : AbstractValidator<WebTicket>
    {
        private readonly WebBetDbContext _context;

        public TicketValidator(WebBetDbContext context)
        {
            _context = context;

            RuleFor(x => x).Must(ticket => IsTopOfferInComibinationWithRegularOffer(ticket))
                             .WithMessage("Top offer match must be in combination with 5 regular offer matches.")

                           .Must(ticket => IsTopOfferCombination(ticket))
                             .WithMessage("Top offer matches cannot combine each other. Only one allowed.")

                           .Must(ticket => AreValidTicketParameters(ticket))
                             .WithMessage("Invalid ticket inputs.")

                           .Must(ticket => ValidateWalletBalance(ticket))
                             .WithMessage("Insufficient funds.");


            RuleFor(x => x.PossibleReturn)
                        .NotNull().WithMessage("{PropertyName} is null")
                        .GreaterThan(2.1).WithMessage("{PropertyName} must be greater than 2 ");

            RuleFor(x => x.Stake)
                        .NotNull().WithMessage("{PropertyName} is null")
                        .GreaterThan(1.99).WithMessage("Minimum {PropertyName} can be 2 ");

            

            RuleFor(x => x.StakeWithManipulationCosts)
                       .NotNull().WithMessage("{PropertyName} is null")
                       .GreaterThan(1.89).WithMessage("Invalid {PropertyName} ");

            RuleFor(x => x.TotalMatchesCoefficient)
                       .NotNull().WithMessage("{PropertyName} is null")
                       .GreaterThan(1).WithMessage("{PropertyName} must be greater than 1 ");

            RuleForEach(x => x.TicketMatches)
                       .SetValidator(new TicketMatchesValidator(_context));
        }

        private bool ValidateWalletBalance(WebTicket ticket)
        {
            return (ticket.UserBalance >= ticket.Stake);
        }

        private bool IsTopOfferInComibinationWithRegularOffer(WebTicket ticket)
        {
            var topOfferMatches = ticket.TicketMatches.Where(m => m.SelectedInTopOffer).ToList();

            return !(topOfferMatches.Count > 0 && ticket.TicketMatches.Count() < 6);
        }

        private bool IsTopOfferCombination(WebTicket ticket)
        {
            return ticket.TicketMatches.Where(m => m.SelectedInTopOffer).ToList().Count <= 1;
        }

        private bool AreValidTicketParameters(WebTicket ticket)
        {
            return (ticket.PossibleReturn > 0 &&
                    ticket.Stake > 0 &&
                    ticket.StakeWithManipulationCosts > 0 &&
                    ticket.TotalMatchesCoefficient > 0 &&
                    ticket.TicketMatches.Count() > 0);
        }
    }

    public class TicketMatchesValidator : AbstractValidator<TicketMatch>
    {
        public TicketMatchesValidator(WebBetDbContext context)
        {

            RuleFor(x => x.Pair)
                .Must(pair => !Check.IsNullOrEmptyOrWhitespace(pair))
                .WithMessage("{PropertyName} must not be null");

            RuleFor(x => x.Type)
                .Must(type => Check.IsValidMatchType(type))
                .WithMessage("Invalid match type");

            RuleFor(x => x.Quota)
                .GreaterThanOrEqualTo(1.1)
                .WithMessage("{PropertyName} must be greater than 1.1.");

            RuleFor(x => x.MatchId)
                .Must(id => MatchExistsInOffer(id, context))
                .WithMessage("Match does not exist offer.");
        }

        private bool MatchExistsInOffer(int matchId, WebBetDbContext db)
        {
            return db.Matches.Any(x => x.Id.Equals(matchId));
        }
    }
}
