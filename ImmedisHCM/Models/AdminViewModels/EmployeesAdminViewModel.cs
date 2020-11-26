using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImmedisHCM.Web.Models
{
    public class EmployeesAdminViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Position")]
        public string JobName { get; set; }

        [Display(Name = "Schedule")]
        public string ScheduleName { get; set; }

        public decimal Salary { get; set; }

        public string Currency { get; set; }

        [Display(Name = "Salary Type")]
        public string SalaryTypeName { get; set; }

        [Display(Name = "Date Hired")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime HiredDate { get; set; }
         
        [Display(Name = "Date Left")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime LeftDate { get; set; }

    }
}
