using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace business_manager_api
{
    [Table(name: "user_account")]
    public class UserAccountModel
    {
        [Index(IsUnique = true)]
        private long id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public GenderEnum Gender { get; set; }

        public DateTime BirthDate { get; set; }

        public bool Profession { get; set; }

    }
    public enum GenderEnum
    {
        Male,
        Female,
        Other
    }

}
