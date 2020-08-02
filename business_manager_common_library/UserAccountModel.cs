using System;

namespace business_manager_common_library
{
    public class UserAccountModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        
        public string Phone { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public bool Profession { get; set; }
        public string Password { get; set; }
    }
    public enum UserGenderEnum
    {
        MALE,
        FEMALE,
        UNKNOWN
    }
    public enum UserTypeEnum
    {
        USER,
        ADMIN
    }
    public enum UserStateEnum
    {
        REVIEWING,
        ACTIVE,
        BLOCKED
    }
}
