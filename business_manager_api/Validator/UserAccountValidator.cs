using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace business_manager_api.Validator
{
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
