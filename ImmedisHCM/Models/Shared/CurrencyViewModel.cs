using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImmedisHCM.Web.Models
{
    public class CurrencyViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Currency")]
        public string Name { get; set; }
    }
}
