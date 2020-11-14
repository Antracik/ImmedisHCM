using System;

namespace ImmedisHCM.Services.Models.Core
{
    public class AttendanceHistoryServiceModel
    {
        public virtual Guid Id { get; set; }
        public virtual EmployeeServiceModel Employee { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual DateTime CheckedIn { get; set; }
        public virtual DateTime? CheckedOut { get; set; }
    }
}
