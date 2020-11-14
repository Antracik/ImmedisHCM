using FluentNHibernate.Mapping;

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
                .CustomType("time")
                .Not.Nullable();

            HasMany(x => x.Jobs);

        }
    }
}
