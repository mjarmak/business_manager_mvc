using business_manager_api.Services;
using FluentValidation;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace business_manager_api
{
    [Table(name: "UserAccount")]
    public class UserAccountDataModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        [Index(IsUnique = true)]
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public bool Profession { get; set; }
        public string Type { get; set; }
        public string State { get; set; }
    }

   
}
