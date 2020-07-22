﻿using business_manager_api.Services;
using business_manager_common_library;
using FluentValidation;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace business_manager_api
{
    [Table(name: "Business")]
    public class BusinessDataModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        //[ForeignKey("Identification")]
        //public long IdentificationId { get; set; }
        public IdentificationData Identification { get; set; }
        public BusinessInfoData BusinessInfo { get; set; }
        public string WorkHours { get; set; }
    }
    [Table(name: "BusinessInfo")]
    public class BusinessInfoData
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
        public string PhotoPath1 { get; set; }
        public string PhotoPath2 { get; set; }
        public string PhotoPath3 { get; set; }
        public string PhotoPath4 { get; set; }
        public string PhotoPath5 { get; set; }
    }
    
    [Table(name: "Identification")]
    public class IdentificationData
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string TVA { get; set; }
        public string EmailPro { get; set; }
        public string Description { get; set; }
        public string LogoPath { get; set; }
    }
    [Table(name: "Address")]
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
        //private readonly string matchError = "Cannot contain any special characters";
        public BusinessDataValidator()
        {
            RuleFor(x => x.WorkHours).NotNull();
        }
    }

    // Fluent Validation for my BusinessDataModel
    public class IdentificationDataValidator : AbstractValidator<IdentificationData>
    {
        private readonly Regex regex = new Regex(@"[^A-Za-z0-9@-_]");
        //private readonly string matchError = "Cannot contain any special characters";
        public IdentificationDataValidator()
        {
            RuleFor(x => x.Name).NotNull();
            //RuleFor(x => TVAClientService.ValidateVAT("BE", x.TVA)).Equal(true).WithMessage("TVA is invalid");
        }
    }

    //Fluent Validation for Business Info
    public class BusinessInfoValidator : AbstractValidator<BusinessInfoData>
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