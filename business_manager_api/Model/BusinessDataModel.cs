using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace business_manager_api
{
    [Table(name: "business_data")]
    public class BusinessDataModel
    {
        [Index(IsUnique = true)]
        public long Id { get; set; }
        public IdentificationData IdentificationData { get; set; }
        public BusinessInfo BusinessInfo { get; set; }
        public string WorkHours { get; set; }
        public List<BusinessImageModel> Images { get; set; }


    }
    public class IdentificationData
    {
        public TypeEnum Type { get; set; }
        public string Name { get; set; }
        public string TVA { get; set; }
        public string EmailPro { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
    }
    public enum TypeEnum
    {
        Bar,
        Club,
        Concert,
        StudentCircle
    }
    public class BusinessInfo
    {
        public AddressData Address { get; set; }
        public string Phone { get; set; }
        public string EmailBusiness { get; set; }
        public string UrlSite { get; set; }
        public string UrlInstagram { get; set; }
        public string UrlFaceBook { get; set; }
        public string UrlLinkedIn { get; set; }
    }
    public class AddressData
    {

    }

}
