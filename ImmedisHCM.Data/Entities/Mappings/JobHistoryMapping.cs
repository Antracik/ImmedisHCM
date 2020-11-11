using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImmedisHCM.Data.Entities.Mappings
{
    public class JobHistoryMapping : ClassMap<JobHistory>
    {
        public JobHistoryMapping()
        {
            Table("jobs_history");

            CompositeId()
                .KeyProperty(x => x.EmployeeId, "employee_id")
                .KeyProperty(x => x.DateChanged, "date_changed");

            References(x => x.SalaryHistory)
                .Columns("salary_history_id", "date_changed")
                .Not.Nullable()
                .ReadOnly();

            References(x => x.Employee)
                .Column("employee_id")
                .Not.Nullable()
                .ReadOnly();

            Map(x => x.FromDate)
                .Column("from_date")
                .Not.Nullable();

            Map(x => x.ToDate)
                .Column("to_date")
                .Not.Nullable();

            References(x => x.Job)
                .Column("job_id");
        }
    }
}
