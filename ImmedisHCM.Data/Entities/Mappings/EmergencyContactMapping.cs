using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ImmedisHCM.Data.Entities.Mappings
{
    public class EmergencyContactMapping : ClassMap<EmergencyContact>
    {
        public EmergencyContactMapping()
        {
            Table("emergency_contacts");

            Id(x => x.Id)
                .Column("id")
                .GeneratedBy.GuidComb();

            Map(x => x.FirstName)
                .Column("first_name")
                .Not.Nullable();

            Map(x => x.LastName)
                .Column("last_name")
                .Not.Nullable();

            Map(x => x.PhoneNumber)
                .Column("phone_number")
                .Not.Nullable();

            Map(x => x.HomePhoneNumber)
                .Column("home_phone_number")
                .Nullable();

            Map(x => x.Email)
                .Column("email")
                .Nullable();

            References(x => x.Location)
                .Column("location_id")
                .Nullable();
        }
    }
}
