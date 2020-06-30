using FluentValidation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public List<BusinessImageModel> Images { get; set; }
    }
    [Table(name: "BusinessInfo")]
    public class BusinessInfo
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public IdentificationData Address { get; set; }
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
    public class BusinessDataValidator : AbstractValidator<BusinessDataModel>
    {
        private readonly Regex regex = new Regex(@"[^A-Za-z0-9@-_]");
        private readonly string matchError = "Cannot contain any special characters";
        public BusinessDataValidator()
        {
            RuleFor(x => x.IdentificationData.Name).NotNull();
        }
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
}