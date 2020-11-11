using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ImmedisHCM.Data.Entities.Mappings
{
    public class CompanyMapping : ClassMap<Company>
    {
        public CompanyMapping()
        {
            Table("companies");
            Id(x => x.Id)
                .GeneratedBy.GuidComb();

            Map(x => x.Name)
                .Column("name")
                .Not.Nullable();

            Map(x => x.LegalName)
                .Column("legal_name")
                .Not.Nullable();

            HasMany(x => x.Departments)
                .KeyColumn("company_id")
                .Inverse()
                .Fetch.Select();
        }
    }
}
