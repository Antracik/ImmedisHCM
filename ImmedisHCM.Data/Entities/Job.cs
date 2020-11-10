using System;

namespace ImmedisHCM.Data.Entities
{
    public class Job
    {
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        public virtual decimal MinimalSalary { get; set; }
        public virtual decimal MaximumSalary { get; set; }
        public virtual ScheduleType ScheduleType { get; set; }
    }
}