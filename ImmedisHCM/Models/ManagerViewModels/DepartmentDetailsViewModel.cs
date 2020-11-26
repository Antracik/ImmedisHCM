using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImmedisHCM.Web.Models
{
    public class DepartmentDetailsViewModel
    {
        [Display(Name = "Department")]
        public string DepartmentName { get; set; }

        public List<EmployeesViewModel> Employees { get; set; }

        public LocationViewModel Address { get; set; }

        public string City { get; set; }
        public string Country { get; set; }

        public string Company { get; set; }

        [Display(Name = "Manager Name")]
        public string ManagerName { get; set; }

        [Display(Name = "Manager Email")]
        public string ManagerEmail { get; set; }


    }
}
