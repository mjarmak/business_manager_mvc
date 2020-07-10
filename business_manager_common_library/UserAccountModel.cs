using System;

namespace business_manager_api
{
    public class UserAccountModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        
        public string Phone { get; set; }
        public GenderEnum Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public bool Profession { get; set; }
        public string Password { get; set; }
    }
    public enum GenderEnum
    {
        Male,
        Female,
        Unknown
    }
}
