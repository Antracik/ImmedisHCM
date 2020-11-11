using FluentNHibernate.Mapping;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ImmedisHCM.Data.Entities.Mappings
{
    public class SalaryHistoryMapping : ClassMap<SalaryHistory>
    {
        public SalaryHistoryMapping()
        {
            Table("salaries_history");

            CompositeId()
                .KeyProperty(x => x.SalaryId, "salary_id")
                .KeyProperty(x => x.DateChanged, "date_changed");

            Map(x => x.Amount)
                .Column("amount")
                .Not.Nullable();

            Map(x => x.FromDate)
                .Column("from_date")
                .Not.Nullable();

            Map(x => x.ToDate)
                .Column("to_date")
                .Not.Nullable();

            References(x => x.Salary)
                .Column("salary_id")
                .Not.Nullable()
                .ReadOnly();

            References(x => x.Employee)
                .Column("employee_id")
                .Not.Nullable();

        }
    }
}
