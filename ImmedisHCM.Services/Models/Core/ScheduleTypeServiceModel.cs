using System;
using System.Collections.Generic;

namespace ImmedisHCM.Services.Models.Core
{
    public class ScheduleTypeServiceModel
    {
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        public virtual int Hours { get; set; }
        public virtual DateTime StartTime { get; set; }
        public virtual IList<JobServiceModel> Jobs { get; set; }
    }
}