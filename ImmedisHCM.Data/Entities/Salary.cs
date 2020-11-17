using System;

namespace ImmedisHCM.Data.Entities
{
    public class Salary : IBaseEntity
    {
        public virtual Guid Id { get; set; }
        public virtual decimal Amount { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual SalaryType SalaryType { get; set; }
        public virtual Currency Currency { get; set; }
    }
}