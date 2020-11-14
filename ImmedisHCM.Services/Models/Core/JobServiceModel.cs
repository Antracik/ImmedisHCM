using System;

namespace ImmedisHCM.Services.Models.Core
{
    public class JobServiceModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal MinimalSalary { get; set; }
        public decimal MaximumSalary { get; set; }
        public ScheduleTypeServiceModel ScheduleType { get; set; }
    }
}