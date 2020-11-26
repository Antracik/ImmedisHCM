using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImmedisHCM.Web.Models
{
    public class CreateCityViewModel
    {
        [Required]
        [Display(Name = "City Name")]
        public string Name { get; set; }
        public List<CountryViewModel> Countries { get; set; }
        
        [Required]
        [Display(Name = "Country")]
        public Guid CountryId { get; set; }

    }
}
