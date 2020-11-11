using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ImmedisHCM.Data.Entities.Mappings
{
    public class JobMapping : ClassMap<Job>
    {
        public JobMapping()
        {
            Table("jobs");

            Id(x => x.Id)
                .Column("id")
                .GeneratedBy.GuidComb();

            Map(x => x.Name)
                .Column("name")
                .Not.Nullable();

            Map(x => x.MinimalSalary)
                .Column("min_salary")
                .Not.Nullable();

            Map(x => x.MaximumSalary)
                .Column("max_salary")
                .Not.Nullable();

            References(x => x.ScheduleType)
                .Column("schedule_type_id")
                .Not.Nullable();
        }
    }
}
