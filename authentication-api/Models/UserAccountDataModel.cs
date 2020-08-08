using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace authentication_api
{
    public class UserAccountDataModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public string BirthDate { get; set; }
        public bool Profession { get; set; }
        public string Password { get; set; }
    }

    public class UserAccountValidator : AbstractValidator<UserAccountDataModel>
    {
        private readonly Regex RegexName = new Regex("^[A-Za-z0-9 ]*$");
        private readonly Regex RegexEmail = new Regex("^[A-Za-z0-9@-_.]*$");
        public UserAccountValidator()
        {
            RuleFor(x => x.Name)
                .Length(1, 50).NotNull().WithMessage("The Name field cannot be empty.")
                .Matches(RegexName).WithMessage("Name cannot contain any special characters");

            RuleFor(x => x.Surname)
                .Length(0, 50).NotNull().WithMessage("Surname field cannot be empty.")
                .Matches(RegexName).WithMessage("Surname cannot contain any special characters");

            RuleFor(x => x.Email)
                .Length(0, 255).NotNull().WithMessage("Email field cannot be empty.")
                .Matches(RegexEmail).WithMessage("Email cannot contain any special characters")
                .EmailAddress().WithMessage("A valid Email is required");

            RuleFor(x => x.Password)
                .Length(0, 255).NotNull().WithMessage("The Password field is empty.")
                .MinimumLength(6);

            RuleFor(x => x.Phone)
                .Length(0, 25);

            //RuleFor(x => PhoneNumberCheckView.IsValidPhoneNumber(x.Phone)).Equal(true).WithMessage("Phone number is invalid");
            //RuleFor(x => PhoneNumberCheckView.IsMobileNumber(x.Phone)).Equal(true).WithMessage("Phone number must be a mobile.");

            RuleFor(x => x.Gender)
                .NotNull().WithMessage("Need to select a gender type.");

            RuleFor(x => x.BirthDate)
                .NotNull().WithMessage("Need to Specify birth date.");

            RuleFor(x => x.Profession)
                .NotNull();
        }
    }
}
