using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using business_manager_api.Domain;
using FluentValidation;

namespace business_manager_api.Validator
{
    // Fluent Validation for my BusinessDataModel
    public class BusinessDataValidator : AbstractValidator<BusinessDataModel>
    {
        //private readonly string matchError = "Cannot contain any special characters";
        public BusinessDataValidator()
        {
            RuleFor(x => x.WorkHours).NotNull();
        }
    }
}
