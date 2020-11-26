using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImmedisHCM.Web.Models
{
    public class DepartmentsAdminViewModel
    {
        [Display(Name = "Department")]
        public string Name { get; set; }

        [Display(Name = "Company")]
        public string CompanyName { get; set; }

        [Display(Name = "Manager")]
        public string ManagerName { get; set; }

        [Display(Name = "Employee cnt")]
        public int EmployeeCount { get; set; }

        public string Address1 { get; set; }
        public string Address2 { get; set; }

        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

    }
}
