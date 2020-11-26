using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImmedisHCM.Web.Models
{
    public class CreateDepartmentViewModel
    {
        [Required]
        [Display(Name = "Department Name")]
        public string Name { get; set; }

        public List<CompanyViewModel> Companies { get; set; }

        public List<CountryViewModel> Countries { get; set; }
        public List<CityViewModel> Cities { get; set; }

        public LocationViewModel Address { get; set; }

        [Required]
        [Display(Name = "City")]
        public Guid CityId { get; set; }
        
        [Display(Name = "Country")]
        public Guid CountryId { get; set; }
        
        [Required]
        [Display(Name = "Company")]
        public Guid CompanyId { get; set; }
    }
}
