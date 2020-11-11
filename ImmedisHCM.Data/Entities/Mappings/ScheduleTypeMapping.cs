using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ImmedisHCM.Data.Entities.Mappings
{
    public class ScheduleTypeMapping : ClassMap<ScheduleType>
    {
        public ScheduleTypeMapping()
        {
            Table("schedule_types");
            Id(x => x.Id)
                .Column("id")
                .GeneratedBy.GuidComb();

            Map(x => x.Name)
                .Column("name")
                .Not.Nullable();

            Map(x => x.Hours)
                .Column("hours")
                .Not.Nullable();

            Map(x => x.StartTime)
                .Column("start_time")
                .Not.Nullable();

            HasMany(x => x.Jobs);

        }
    }
}
