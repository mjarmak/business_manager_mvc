using PhoneNumbers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace business_manager_api.Services
{
    public class PhoneNumberCheckViewModel
    {
        private string _countryCodeSelected;

        [Required]
        [Display(Name = "Issuing Country")]
        public string CountryCodeSelected
        {
            get => _countryCodeSelected;
            set => _countryCodeSelected = value.ToUpperInvariant();
        }

        [Required]
        [Display(Name = "Number to Check")]
        public string PhoneNumberRaw { get; set; }

        // Holds the validation response. Not for data entry.
        [Display(Name = "Valid Number")]
        public bool Valid { get; set; }

        // Holds the validation response. Not for data entry.
        [Display(Name = "Has Extension")]
        public bool HasExtension { get; set; }

        // Optionally, add more fields here for returning data to the user.


    }
}
