using System.Text.RegularExpressions;
using business_manager_api.Services;
using FluentValidation;

namespace business_manager_api.Validator
{
    public class UserAccountValidator : AbstractValidator<UserAccountDataModel>
    {
        private readonly Regex _regex = new Regex(@"[^A-Za-z0-9@-_]");
        private const string MatchError = "Cannot contain any special characters";

        public UserAccountValidator()
        {
            RuleFor(x => x.Name)
                .Length(1, 50).NotNull().WithMessage("The Name field is empty.")
                .Matches(_regex).WithMessage(MatchError);

            RuleFor(x => x.Surname)
                .Length(0, 50).NotNull().WithMessage("The Surname field is empty.")
                .Matches(_regex).WithMessage(MatchError);

            RuleFor(x => x.Email)
                .Length(0, 255).NotNull().WithMessage("The Email field is empty.")
                .EmailAddress().WithMessage("A valid email is required")
                .Matches(_regex).WithMessage(MatchError);

            RuleFor(x => x.Phone)
                .Length(0, 25);

            RuleFor(x => PhoneNumberCheckView.IsValidPhoneNumber(x.Phone)).Equal(true).WithMessage("Phone number is invalid");
            RuleFor(x => PhoneNumberCheckView.IsMobileNumber(x.Phone)).Equal(true).WithMessage("Phone number must be a mobile.");

            RuleFor(x => x.Gender)
                .NotNull().WithMessage("Need to select a gender type.");

            RuleFor(x => x.BirthDate)
                .NotNull();

            RuleFor(x => x.Profession)
                .NotNull();
        }
    }
}
