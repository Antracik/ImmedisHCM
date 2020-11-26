using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImmedisHCM.Web.Models
{
    public class CreateCountryViewModel
    {
        [Required]
        [Display(Name = "Country")]
        public string Name { get; set; }
        
        [Required]
        [Display(Name = "Short Name")]
        public string ShortName { get; set; }

    }
}
