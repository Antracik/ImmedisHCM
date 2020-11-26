using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImmedisHCM.Web.Models
{
    public class CreateEmployeeViewModel
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        [Phone]
        public string PhoneNumber { get; set; }

        public LocationViewModel Address { get; set; }
        
        public List<CountryViewModel> Countries { get; set; }
        public List<CityViewModel> Cities { get; set; }

        [Display(Name = "Country")]
        public Guid CountryId { get; set; }

        [Required]
        [Display(Name = "City")]
        public Guid CityId { get; set; }

        public List<CompanyViewModel> Companies { get; set; }
        public List<DepartmentViewModel> Departments { get; set; }

        [Display(Name = "Company")]
        public Guid CompanyId { get; set; }


        [Required]
        [Display(Name = "Department")]
        public Guid DepartmentId { get; set; }
        
        public List<JobViewModel> Jobs { get; set; }

        [Required]
        [Display(Name = "Position Title")]
        public Guid JobId { get; set; }

        [Required]
        [Display(Name = "Register as Manager (Gives the Manager role)")]
        public bool IsManager { get; set; }

        [Display(Name = "Salary Amount")]
        [Range(1,double.MaxValue, ErrorMessage = "Salary cannot be negative")]
        public decimal SalaryAmount { get; set; }

        public List<SalaryTypeViewModel> SalaryTypes { get; set; }

        [Required]
        [Display(Name = "Salary Type")]
        public int SalaryTypeId { get; set; }

        public List<CurrencyViewModel> Currencies { get; set; }

        [Required]
        [Display(Name = "Currency")]
        public int CurrencyId { get; set; }

        public string StatusMessage { get; set; }
    }
}
