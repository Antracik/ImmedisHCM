﻿using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ImmedisHCM.Data.Entities.Mappings
{
    public class CityMapping : ClassMap<City>
    {
        public CityMapping()
        {
            Table("cities");
            Id(x => x.Id)
                .Column("id")
                .GeneratedBy.GuidComb();

            Map(x => x.Name)
                .Column("name")
                .Not.Nullable();

            References(x => x.Country)
                .Columns("country_id")
                .Not.Nullable()
                .Fetch.Join();
        }
    }
}
