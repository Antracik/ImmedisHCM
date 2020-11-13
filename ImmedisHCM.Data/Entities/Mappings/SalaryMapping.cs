﻿using FluentNHibernate.Mapping;
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

            References(x => x.SalaryType)
                .Column("salary_type_id")
                .Not.Nullable();

            References(x => x.Currency)
                .Column("currency_id")
                .Not.Nullable();

            HasOne(x => x.Employee)
                .ForeignKey("salary_id");
                

        }
    }
}
