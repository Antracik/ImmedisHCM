using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImmedisHCM.Web.Models
{
    public class CompanyAdminViewModel
    {
        [Display(Name = "Company")]
        public string Name { get; set; }

        [Display(Name = "Legal Name")]
        public string LegalName { get; set; }

        [Display(Name = "Dept. count")]
        public int DepartmentCount { get; set; }
    }
}
