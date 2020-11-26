using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImmedisHCM.Web.Models
{
    public class EmployeeDetailsViewModel
    {
        [Display(Name = "Full name")]
        public string EmployeeName { get; set; }
    
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        [Display(Name = "Date Hired")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime HiredDate { get; set; }
        public LocationViewModel Address { get; set; }
        
        [Display(Name = "Country/City")]
        public string CityCountry { get; set; }

        //job
        [Display(Name = "Position")]
        public string JobName { get; set; }

        [Display(Name = "Schedule")]
        public virtual string ScheduleName { get; set; }

        [Display(Name = "Work Time")]
        public virtual int ScheduleHours { get; set; }

        [Display(Name = "Start Time")]
        [DisplayFormat(DataFormatString = "{0:hh:mm}")]
        public virtual DateTime ScheduleStartTime { get; set; }

        //salary
        [Display(Name ="Salary")]
        public string Amount { get; set; }

        [Display(Name = "Salary Type")]
        public string SalaryTypeName { get; set; }

        //department
        [Display(Name = "Department")]
        public string DepartmentName { get; set; }

        [Display(Name = "Company")]
        public string CompanyName { get; set; }

        //emergency contact
        [Display(Name = "Name")]
        public string EmergencyName { get; set; }

        [Display(Name = "Phone Number")]
        public string EmergencyPhoneNumber { get; set; }

        [Display(Name = "Home Phone Number")]
        public string EmergencyHomePhoneNumber { get; set; }

        [Display(Name = "Email")]
        public string EmergencyEmail { get; set; }
        public LocationViewModel EmergencyAddress { get; set; }
        

    }
}
