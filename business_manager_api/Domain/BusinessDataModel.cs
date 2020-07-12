using business_manager_api.Services;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Text.RegularExpressions;

namespace business_manager_api
{
    [Table(name: "BusinessData")]
    public class BusinessDataModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public IdentificationData IdentificationData { get; set; }
        public BusinessInfo BusinessInfo { get; set; }
        public string WorkHours { get; set; }
    }
    [Table(name: "BusinessInfo")]
    public class BusinessInfo
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public AddressData Address { get; set; }
        public string Phone { get; set; }
        public string EmailBusiness { get; set; }
        public string UrlSite { get; set; }
        public string UrlInstagram { get; set; }
        public string UrlFaceBook { get; set; }
        public string UrlLinkedIn { get; set; }
    }
    public enum BusinessTypeEnum
    {
        Bar,
        Club,
        Concert,
        StudentCircle
    }
    
    [Table(name: "IdentificationData")]
    public class IdentificationData
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public BusinessTypeEnum Type { get; set; }
        public string Name { get; set; }
        public string TVA { get; set; }
        public string EmailPro { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
    }
    [Table(name: "AddressData")]
    public class AddressData
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Street { get; set; }
        public string BoxNumber { get; set; }
    }

    // Fluent Validation for my BusinessDataModel
    public class BusinessDataValidator : AbstractValidator<BusinessDataModel>
    {
        private readonly Regex regex = new Regex(@"[^A-Za-z0-9@-_]");
        //private readonly string matchError = "Cannot contain any special characters";
        public BusinessDataValidator()
        {
            RuleFor(x => x.IdentificationData.Name).NotNull();
        }
        

    }


    //Fluent Validation for Business Info
    public class BusinessInfoValidator : AbstractValidator<BusinessInfo>
    {
        private readonly Regex regex = new Regex(@"[^A-Za-z0-9@-_]");
        //private readonly string matchError = "Cannot contain any special characters";
        public BusinessInfoValidator()
        {
            RuleFor(s => s.EmailBusiness).NotEmpty().WithMessage("Email address is required.")
                     .EmailAddress().WithMessage("A valid email is required.");

            RuleFor(x => x.Phone)
                .Length(0, 25);

            RuleFor(x => PhoneNumberCheckView.IsValidPhoneNumber(x.Phone)).Equal(true).WithMessage("Phone number is invalid");
            RuleFor(x => PhoneNumberCheckView.IsMobileNumber(x.Phone)).Equal(true).WithMessage("Phone number must be a mobile.");

            RuleFor(x => Uri.IsWellFormedUriString(x.UrlSite, UriKind.RelativeOrAbsolute)).Equal(true).WithMessage("Url not formatted correctly");
            RuleFor(x => Uri.IsWellFormedUriString(x.UrlInstagram, UriKind.RelativeOrAbsolute)).Equal(true).WithMessage("Url Instagram not formatted correctly");
            RuleFor(x => Uri.IsWellFormedUriString(x.UrlFaceBook, UriKind.RelativeOrAbsolute)).Equal(true).WithMessage("Url Facebook not formatted correctly");
            RuleFor(x => Uri.IsWellFormedUriString(x.UrlLinkedIn, UriKind.RelativeOrAbsolute)).Equal(true).WithMessage("Url LinkedIn not formatted correctly");

        }


    }


}