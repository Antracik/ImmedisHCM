using System;
using System.Collections.Generic;
using System.Text;

namespace ImmedisHCM.Data.Entities
{
    public class ScheduleType
    {
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        public virtual int Hours { get; set; }
        public virtual DateTime StartTime { get; set; }
    }
}
