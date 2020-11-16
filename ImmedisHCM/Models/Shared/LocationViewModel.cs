using System.ComponentModel.DataAnnotations;

namespace ImmedisHCM.Web.Models
{
    public class LocationViewModel
    {
        [Required(ErrorMessage = "Please enter at least one Address Line")]
        [Display(Name ="Address Line 1")]
        public string AddressLine1 { get; set; }

        [Display(Name = "Address Line 2")]
        public string AddressLine2 { get; set; }

        [Required]
        [DataType(DataType.PostalCode)]
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }
    }
}