using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImmedisHCM.Web.Models
{
    public class CreateSalaryTypeViewModel
    {
        [Required]
        [Display(Name = "Salary Type name")]
        public string Name { get; set; }
    }
}
