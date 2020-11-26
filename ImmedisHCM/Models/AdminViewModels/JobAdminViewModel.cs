using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImmedisHCM.Web.Models
{
    public class JobAdminViewModel
    {
        [Display(Name = "Job Title")]
        public string Name { get; set; }
        
        [Display(Name = "Min salary")]
        public decimal MinimalSalary { get; set; }

        [Display(Name = "Max salary")]
        public decimal MaximumSalary { get; set; }

        [Display(Name = "Schedule")]
        public string ScheduleName { get; set; }

        [Display(Name = "Work Time")]
        public int Hours { get; set; }

        [Display(Name = "Job Title")]
        [DisplayFormat(DataFormatString = "{0:hh:mm}")]
        public DateTime StartTime { get; set; }
    }
}
