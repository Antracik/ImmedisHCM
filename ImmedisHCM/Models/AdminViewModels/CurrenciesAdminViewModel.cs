using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImmedisHCM.Web.Models
{
    public class CurrenciesAdminViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Currency")]
        public string Name { get; set; }
        
        [Display(Name = "Salary cnt")]
        public int SalaryCount { get; set; }
    }
}
