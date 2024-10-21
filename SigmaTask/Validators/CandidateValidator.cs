using FluentValidation;
using SigmaTask.Data.Entities;
using System.Text.RegularExpressions;

namespace SigmaTask.Validators;

public partial class CandidateValidator : AbstractValidator<Candidate>
{
    public CandidateValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Valid email is required.");

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("FirstName is required.");

        RuleFor(x => x.PhoneNumber)
            .Matches(ValidPhoneNumberRegex())
            .WithMessage("PhoneNumber not valid");

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("LastName is required.");

        RuleFor(x => x.Comment)
            .NotEmpty()
            .WithMessage("Comment is required.");
    }

    [GeneratedRegex(@"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}")]
    private static partial Regex ValidPhoneNumberRegex();
}
