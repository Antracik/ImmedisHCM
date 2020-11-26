using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImmedisHCM.Web.Models
{
    public class CreateScheduleTypeViewModel
    {
        [Required]
        [Display(Name = "Schedule Name")]
        public string Name { get; set; }

        [Required]
        [Range(1, 24)]
        [Display(Name = "Work time")]
        public int Hours { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "Start Time")]
        public DateTime StartTime {get; set;}
    }
}
