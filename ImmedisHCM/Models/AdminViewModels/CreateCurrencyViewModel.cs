using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImmedisHCM.Web.Models
{
    public class CreateCurrencyViewModel
    {
        [Required]
        [Display(Name ="Currency Name")]
        public string Name { get; set; }
    }
}
