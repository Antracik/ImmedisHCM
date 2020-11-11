using FluentNHibernate.Mapping;
using FluentNHibernate.MappingModel.Collections;
using NHibernate.Linq;
using NHibernate.Type;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

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
