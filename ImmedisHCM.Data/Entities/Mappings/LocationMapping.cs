﻿using FluentNHibernate.Conventions;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ImmedisHCM.Data.Entities.Mappings
{
    public class LocationMapping : ClassMap<Location>
    {
        public LocationMapping()
        {
            Table("locations");

            Id(x => x.Id)
                .Column("id")
                .GeneratedBy.GuidComb();

            Map(x => x.AddressLine1)
                .Column("address_line1")
                .Not.Nullable();

            Map(x => x.AddressLine2)
                .Column("address_line2")
                .Nullable();

            Map(x => x.PostalCode)
                .Column("postal_code")
                .Not.Nullable();

            References(x => x.City)
                .Column("city_id")
                .Nullable();

        }
    }
}
