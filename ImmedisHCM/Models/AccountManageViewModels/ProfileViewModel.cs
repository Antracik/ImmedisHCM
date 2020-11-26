using System.ComponentModel.DataAnnotations;

namespace ImmedisHCM.Web.Models
{
    public class ProfileViewModel
    {
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public CityViewModel City { get; set; }
        public CountryViewModel Country { get; set; }
        public LocationViewModel Location { get; set; }
        public string StatusMessage { get; set; }
    }

    public class ManagerViewModel
    {
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string Email { get; set; }

        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

    }
}
