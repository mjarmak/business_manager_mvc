using System.Collections.Generic;

namespace business_manager_common_library
{
    public class BusinessModel
    {
        public long Id { get; set; }
        public bool Disabled { get; set; }
        public Identification Identification { get; set; }
        public BusinessInfo BusinessInfo { get; set; }
        public List<WorkHours> WorkHours { get; set; }
    }
    public class BusinessInfo
    {
        public long Id { get; set; }
        public Address Address { get; set; }
        public string Phone { get; set; }
        public string EmailBusiness { get; set; }
        public string UrlSite { get; set; }
        public string UrlInstagram { get; set; }
        public string UrlFaceBook { get; set; }
        public string UrlLinkedIn { get; set; }
    }
    public enum BusinessTypeEnum
    {
        BAR,
        CLUB,
        CONCERT,
        STUDENTCIRCLE
    }
   
    public class Identification
    {
        public long Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string TVA { get; set; }
        public string EmailPro { get; set; }
        public string Description { get; set; }
    }
    public class Address
    {
        public long Id { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Street { get; set; }
        public string BoxNumber { get; set; }
    }
    public class WorkHours
    {
        public long Id { get; set; }
        public long BusinessId { get; set; }
        public string Day { get; set; }
        public int HourFrom { get; set; }
        public int HourTo { get; set; }
        public int MinuteTo { get; set; }
        public int MinuteFrom { get; set; }
        public bool Closed { get; set; }
    }
    public enum WorkHoursDayEnum
    {
        MONDAY,
        TUESDAY,
        WEDNESDAY,
        THURSDAY,
        FRIDAY,
        SATURDAY,
        SUNDAY
    }
}