using System;

namespace ImmedisHCM.Services.Models.Core
{
    public class JobHistoryServiceModel
    {
        public virtual Guid EmployeeId { get; set; }
        public virtual EmployeeServiceModel Employee { get; set; }
        public virtual JobServiceModel Job { get; set; }
        public virtual SalaryHistoryServiceModel SalaryHistory { get; set; }
        public virtual DateTime DateChanged { get; set; }
        public virtual DateTime FromDate { get; set; }
        public virtual DateTime ToDate { get; set; }
    }
}
