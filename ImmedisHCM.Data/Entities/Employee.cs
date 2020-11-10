using System;

namespace ImmedisHCM.Data.Entities
{
    public class Employee
    {
        public virtual Guid Id { get; set; }
        public virtual Guid HrId { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string PhoneNumber { get; set; }
        public virtual string Email { get; set; }
        public virtual DateTime HiredDate { get; set; }
        public virtual DateTime? LeftDate { get; set; }
        public virtual EmergencyContact EmergencyContact { get; set; }
        public virtual Job Job { get; set; }
        public virtual Location Location { get; set; }
        public virtual Salary Salary { get; set; }
        public virtual Department Department { get; set; }
        public virtual Employee Manager { get; set; }

    }
}