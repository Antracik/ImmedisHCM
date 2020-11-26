using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImmedisHCM.Web.Models
{
    public class CitiesAdminViewModel
    {
        [Display(Name = "City")]
        public string Name { get; set; }
        public string Country { get; set; }
    }
}
