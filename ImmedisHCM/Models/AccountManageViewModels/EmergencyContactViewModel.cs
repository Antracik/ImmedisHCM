using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImmedisHCM.Web.Models
{
    public class EmergencyContactViewModel
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        
        [Required]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [Phone]
        public string PhoneNumber { get; set; }

        [Display(Name = "Home Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [Phone]
        public string HomePhoneNumber { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public LocationViewModel Location { get; set; }

        public List<CountryViewModel> Countries { get; set; }
        public List<CityViewModel> Cities { get; set; }

        [Required]
        [Display(Name = "Country")]
        public Guid CountryId { get; set; }

        [Required]
        [Display(Name = "City")]
        public Guid CityId { get; set; }

        public string StatusMessage { get; set; }
    }
}
