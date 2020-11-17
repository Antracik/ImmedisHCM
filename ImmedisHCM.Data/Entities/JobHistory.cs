using NHibernate.Linq.Visitors.ResultOperatorProcessors;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImmedisHCM.Data.Entities
{
    public class JobHistory : IBaseEntity
    {
        public virtual Guid EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Job Job { get; set; }
        public virtual SalaryHistory SalaryHistory { get; set; }
        public virtual DateTime DateChanged { get; set; }
        public virtual DateTime FromDate { get; set; }
        public virtual DateTime ToDate { get; set; }

        public override bool Equals(object obj)
        {
            return obj is JobHistory history &&
                   EmployeeId.Equals(history.EmployeeId) &&
                   DateChanged == history.DateChanged;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(EmployeeId, DateChanged);
        }

    }
}
