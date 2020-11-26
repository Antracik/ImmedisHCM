using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImmedisHCM.Web.Models
{
    public class JobViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Position Title")]
        public string Name { get; set; }
    }
}
