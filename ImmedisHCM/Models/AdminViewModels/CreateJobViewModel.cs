using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImmedisHCM.Web.Models
{
    public class CreateJobViewModel
    {
        [Required]
        [Display(Name = "Job Title")]
        public string Name { get; set; }
        
        [Required]
        [Display(Name = "Minimal Salary")]
        [Range(1, double.MaxValue, ErrorMessage = "Minimal Salary cannot be less than 1")]
        public decimal MinimalSalary { get; set; }
        
        [Required]
        [Display(Name = "Maximum Salary")]
        [Range(1, double.MaxValue, ErrorMessage = "Maximum Salary cannot be less than 1")]
        public decimal MaximumSalary { get; set; }
        public List<ScheduleTypeViewModel> Schedules { get; set; }

        [Required]
        [Display(Name = "Schedule")]
        public Guid ScheduleId { get; set; }

    }
}
