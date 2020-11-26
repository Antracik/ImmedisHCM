using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImmedisHCM.Web.Models
{
    public class ScheduleAdminViewModel
    {
        [Display(Name = "Schedule Name")]
        public string ScheduleName { get; set; }

        [Display(Name = "Work Time")]
        public int Hours { get; set; }

        [Display(Name = "Start Time")]
        [DisplayFormat(DataFormatString = "{0:hh:mm}")]
        public DateTime StartTime { get; set; }

        [Display(Name = "Job Count")]
        public int JobCount { get; set; }
    }
}
