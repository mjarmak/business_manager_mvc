using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
// ReSharper disable All

namespace business_manager_api.Domain
{
    [Table(name: "Business")]
    public class BusinessDataModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public bool Disabled { get; set; }
        public IdentificationData Identification { get; set; }
        [Display(Name = "Business Info")]
        public BusinessInfoData BusinessInfo { get; set; }
        [Display(Name = "Work hours")]
        public List<WorkHoursData> WorkHours { get; set; }
    }
    [Table(name: "BusinessInfo")]
    public class BusinessInfoData
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public AddressData Address { get; set; }
        [Display(Name = "Phone number")]
        public string Phone { get; set; }

        [Display(Name = "Professional Email")]
        public string EmailBusiness { get; set; }
        [Display(Name = "Website")]
        public string UrlSite { get; set; }
        [Display(Name = "Instagram Page")]
        public string UrlInstagram { get; set; }
        [Display(Name = "Facebook Page")]
        public string UrlFaceBook { get; set; }
        [Display(Name = "Linkedin Page")]
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
        [Display(Name = "Professional Email")]
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
    [Table(name: "WorkHours")]
    public class WorkHoursData
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Day { get; set; }
        public int HourFrom { get; set; }
        public int HourTo { get; set; }
        public int MinuteTo { get; set; }
        public int MinuteFrom { get; set; }
        public bool Closed { get; set; }
    }



}