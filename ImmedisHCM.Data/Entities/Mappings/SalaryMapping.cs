using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ImmedisHCM.Data.Entities.Mappings
{
    public class SalaryMapping : ClassMap<Salary>
    {
        public SalaryMapping()
        {
            Table("salaries");

            Id(x => x.Id)
                .Column("id")
                .GeneratedBy.GuidComb();

            Map(x => x.Amount)
                .Column("amount")
                .Not.Nullable();

            HasOne(x => x.SalaryType);
            HasOne(x => x.Currency);

        }
    }
}
