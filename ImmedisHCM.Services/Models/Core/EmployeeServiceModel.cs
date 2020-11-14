using System;

namespace ImmedisHCM.Services.Models.Core
{
    public class EmployeeServiceModel
    {
        public Guid Id { get; set; }
        public Guid HrId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime HiredDate { get; set; }
        public DateTime? LeftDate { get; set; }
        public EmergencyContactServiceModel EmergencyContact { get; set; }
        public JobServiceModel Job { get; set; }
        public LocationServiceModel Location { get; set; }
        public SalaryServiceModel Salary { get; set; }
        public DepartmentServiceModel Department { get; set; }
        public EmployeeServiceModel Manager { get; set; }
    }
}
