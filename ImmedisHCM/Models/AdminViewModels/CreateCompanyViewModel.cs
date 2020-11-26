using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImmedisHCM.Web.Models
{
    public class CreateCompanyViewModel
    {
        [Required]
        [Display(Name = "Company")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Legal Name")]
        public string LegalName { get; set; }
    }
}
