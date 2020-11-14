using FluentNHibernate.Mapping;

namespace ImmedisHCM.Data.Entities.Mappings
{
    public class AttendanceHistoryMapping : ClassMap<AttendanceHistory>
    {
        public AttendanceHistoryMapping()
        {
            Table("attendance_history");

            Id(x => x.Id)
                .Column("id")
                .GeneratedBy.GuidComb();

            Map(x => x.Date)
                .Column("date")
                .Not.Nullable();

            Map(x => x.CheckedIn)
                .Column("checked_in")
                .Not.Nullable();

            Map(x => x.CheckedOut)
                .Column("checked_out")
                .Nullable();

            References(x => x.Employee)
                .Column("employee_id")
                .Not.Nullable();
        }
    }
}
