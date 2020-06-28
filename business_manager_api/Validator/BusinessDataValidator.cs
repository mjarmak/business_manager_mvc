using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace business_manager_api.Validator
{
    public class BusinessDataValidator : AbstractValidator<BusinessDataModel>
    {
        private readonly Regex regex = new Regex(@"[^A-Za-z0-9@-_]");
        private readonly string matchError = "Cannot contain any special characters";
        public BusinessDataValidator()
        {
            RuleFor(x => x.IdentificationData.Name)
                .NotNull().WithMessage("The field ") ;
        }
    }
}
