using System;
using System.Text.RegularExpressions;
using business_manager_api.Domain;
using business_manager_api.Services;
using FluentValidation;
using FluentValidation.Validators;

// ReSharper disable All

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

    //Fluent Validation for Business Info
    public class BusinessInfoValidator : AbstractValidator<BusinessInfoData>
    {
        private readonly Regex _regexName = new Regex("^[A-Za-z0-9 ]*$");
        private readonly Regex _regexEmail = new Regex("^[A-Za-z0-9@-_.]*$");
        public BusinessInfoValidator()
        {
            RuleFor(e => e.EmailBusiness)
                .Length(0, 255).NotNull().WithMessage("The Email field cannot be empty.")
                .Matches(_regexEmail).WithMessage("Email cannot contain any special characters")
                .EmailAddress().WithMessage("A valid email is required");

            RuleFor(x => PhoneNumberCheckView.IsValidPhoneNumber(x.Phone)).Equal(true)
                .WithMessage("Phone number is invalid");
            RuleFor(x => PhoneNumberCheckView.IsMobileNumber(x.Phone)).Equal(true)
                .WithMessage("Phone number must be a mobile.");

            RuleFor(x => Uri.IsWellFormedUriString(x.UrlSite, UriKind.RelativeOrAbsolute))
                .Equal(true)
                .WithMessage("Url not formatted correctly");
            RuleFor(x => Uri.IsWellFormedUriString(x.UrlInstagram, UriKind.RelativeOrAbsolute))
                .Equal(true)
                .WithMessage("Url Instagram not formatted correctly");
            RuleFor(x => Uri.IsWellFormedUriString(x.UrlFaceBook, UriKind.RelativeOrAbsolute))
                .Equal(true).WithMessage("Url Facebook not formatted correctly");
            RuleFor(x => Uri.IsWellFormedUriString(x.UrlLinkedIn, UriKind.RelativeOrAbsolute))
                .Equal(true).WithMessage("Url LinkedIn not formatted correctly");

        }

    }

    // Fluent Validation for my BusinessDataModel
    public class IdentificationDataValidator : AbstractValidator<IdentificationData>
    {
        private readonly Regex _regex = new Regex(@"[^A-Za-z0-9@-_]");
        //private readonly string matchError = "Cannot contain any special characters";
        public IdentificationDataValidator()
        {
            RuleFor(x => x.Name).NotNull();

            RuleFor(x => x.TVA).NotNull();
            //RuleFor(x => TVAClientService.ValidateVAT("BE", x.TVA))
            //    .Equal(true)
            //    .WithMessage("The TVA number is invalid");
            RuleFor(e => e.EmailPro)
                .EmailAddress(EmailValidationMode.Net4xRegex)
                .WithMessage("invalid email address");
            RuleFor(x => x.Description)
                .MaximumLength(1000)
                .WithMessage("Maximum length is 1000 Characters");
        }
    }
}
