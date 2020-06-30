using FluentValidation;

namespace business_manager_api
{
    public class BusinessImageValidator : AbstractValidator<BusinessImageModel>
    {
        private readonly int sizeLimit = 3;
        public BusinessImageValidator()
        {
            RuleFor(x => (x.ImageData.Length <= sizeLimit * 1.37 * 1024 * 1024)).Equal(true);
        }
    }
}
