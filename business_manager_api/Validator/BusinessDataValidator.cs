using FluentValidation;
using System.Text.RegularExpressions;

namespace business_manager_api.Validator
{
    public class BusinessDataValidator : AbstractValidator<BusinessDataModel>
    {
        private readonly Regex regex = new Regex(@"[^A-Za-z0-9@-_]");
        private readonly string matchError = "Cannot contain any special characters";
        public BusinessDataValidator()
        {
            RuleFor(x => x.IdentificationData.Name).NotNull();
        }
    }
}
