using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace business_manager_api
{
    [Table(name: "UserAccount")]
    public class UserAccountModel
    {
        [Key]
        private long Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        [Index(IsUnique = true)]
        public string Email { get; set; }

        public string Phone { get; set; }

        public GenderEnum Gender { get; set; }

        public DateTime BirthDate { get; set; }

        public bool Profession { get; set; }

    }
    public enum GenderEnum
    {
        Male,
        Female,
        Other
    }

    public class UserAccountValidator : AbstractValidator<UserAccountModel>
    {
        private readonly Regex regex = new Regex(@"[^A-Za-z0-9@-_]");
        private readonly string matchError = "Cannot contain any special characters";
        public UserAccountValidator()
        {
            RuleFor(x => x.Name).Length(0, 50).NotNull().Matches(regex).WithMessage(matchError);
            RuleFor(x => x.Surname).Length(0, 50).NotNull().Matches(regex).WithMessage(matchError);
            RuleFor(x => x.Email).Length(0, 255).NotNull().EmailAddress().Matches(regex).WithMessage(matchError);
            RuleFor(x => x.Phone).Length(0, 25);
            RuleFor(x => x.Gender).NotNull();
            RuleFor(x => x.BirthDate).NotNull();
            RuleFor(x => x.Profession).NotNull();
        }
    }
}
