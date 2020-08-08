using FluentValidation;
using System;
using System.Text.RegularExpressions;

namespace business_manager_common_library
{
    public class UserUpdateModel
    {
        public string NameNew { get; set; }
        public string SurnameNew { get; set; }
        public string PhoneNew { get; set; }
        public bool ProfessionNew { get; set; }
        public string NamePrevious { get; set; }
        public string SurnamePrevious { get; set; }
        public string PhonePrevious { get; set; }
        public bool ProfessionPrevious { get; set; }
    }
    public class UserUpdateValidator : AbstractValidator<UserUpdateModel>
    {
        private readonly Regex RegexName = new Regex("^[A-Za-z0-9 ]*$");
        private readonly Regex RegexEmail = new Regex("^[A-Za-z0-9@-_.]*$");
        public UserUpdateValidator()
        {
            RuleFor(x => x.NameNew)
                .Length(1, 50).NotNull().WithMessage("The Name field cannot be empty.")
                .Matches(RegexName).WithMessage("Name cannot contain any special characters");

            RuleFor(x => x.SurnameNew)
                .Length(0, 50).NotNull().WithMessage("Surname field cannot be empty.")
                .Matches(RegexName).WithMessage("Surname cannot contain any special characters");

            RuleFor(x => x.PhoneNew)
                .Length(0, 25);

            RuleFor(x => x.ProfessionNew)
                .NotNull();
        }
    }
}
