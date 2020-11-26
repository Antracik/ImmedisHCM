using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImmedisHCM.Web.Models
{
    public class CountriesAdminViewModel
    {
        [Display(Name = "Country")]
        public string Name { get; set; }
        
        [Display(Name = "Short Name")]
        public string ShortName { get; set; }

        [Display(Name = "City count")]
        public int CityCount { get; set; }

    }
}
