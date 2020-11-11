using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ImmedisHCM.Data.Entities.Mappings
{
    public class SalaryTypeMapping : ClassMap<SalaryType>
    {
        public SalaryTypeMapping()
        {
            Table("salary_type");

            Id(x => x.Id)
                .Column("id")
                .GeneratedBy.Increment();

            Map(x => x.Name)
                .Column("name")
                .Not.Nullable();

            HasMany(x => x.Salaries);
        }
    }
}
