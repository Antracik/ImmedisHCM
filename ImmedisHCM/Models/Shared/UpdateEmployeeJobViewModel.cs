using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImmedisHCM.Web.Models
{
    public class UpdateEmployeeJobViewModel
    {
        [Required]
        public decimal Amount { get; set; }

        [Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }

        [Required]
        public string EmployeeEmail { get; set; }

        public List<SalaryTypeViewModel> SalaryTypes { get; set; }

        [Required]
        [Display(Name = "Salary Type")]
        public int SalaryTypeId { get; set; }

        public List<CurrencyViewModel> Currencies { get; set; }

        [Required]
        [Display(Name = "Currency")]
        public int CurrencyId { get; set; }

        public List<JobViewModel> Jobs { get; set; }

        [Required]
        [Display(Name = "Job")]
        public Guid JobId { get; set; }

        public string StatusMessage { get; set; }
    }
}
