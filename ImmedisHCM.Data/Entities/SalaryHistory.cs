
using NHibernate.Linq.Visitors.ResultOperatorProcessors;
using System;

namespace ImmedisHCM.Data.Entities
{
    public class SalaryHistory : IBaseEntity
    {
        public virtual DateTime DateChanged { get; set; }
        public virtual Guid SalaryId { get; set; }
        public virtual Salary Salary { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual decimal Amount { get; set; }
        public virtual DateTime FromDate { get; set; }
        public virtual DateTime ToDate { get; set; }

        public override bool Equals(object obj)
        {
            return obj is SalaryHistory history &&
                   SalaryId.Equals(history.SalaryId) &&
                   DateChanged == history.DateChanged;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(SalaryId, DateChanged);
        }
    }
}
