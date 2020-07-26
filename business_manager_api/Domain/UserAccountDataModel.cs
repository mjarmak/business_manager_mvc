using business_manager_api.Services;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace business_manager_api
{
    [Table(name: "UserAccount")]
    public class UserAccountDataModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        [Index(IsUnique = true)]
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public bool Profession { get; set; }
        public string Type { get; set; }
        public string State { get; set; }
    }

    public class UserAccountValidator : AbstractValidator<UserAccountDataModel>
    {
        private readonly Regex regex = new Regex(@"[^A-Za-z0-9@-_]");
        private readonly string matchError = "Cannot contain any special characters";
        public UserAccountValidator()
        {
            RuleFor(x => x.Name)
                .Length(1, 50).NotNull().WithMessage("The Name field is empty.")
                .Matches(regex).WithMessage(matchError);

            RuleFor(x => x.Surname)
                .Length(0, 50).NotNull().WithMessage("The Surname field is empty.")
                .Matches(regex).WithMessage(matchError);

            RuleFor(x => x.Email)
                .Length(0, 255).NotNull().WithMessage("The Email field is empty.")
                .EmailAddress().WithMessage("A valid email is required")
                .Matches(regex).WithMessage(matchError);

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
