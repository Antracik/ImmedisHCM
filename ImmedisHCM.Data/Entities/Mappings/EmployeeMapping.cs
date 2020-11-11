using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ImmedisHCM.Data.Entities.Mappings
{
    public class EmployeeMapping : ClassMap<Employee>
    {
        public EmployeeMapping()
        {
            Table("employee");

            Id(x => x.Id)
                .Column("id")
                .GeneratedBy.GuidComb();

            Map(x => x.HrId)
                .Column("hr_id")
                .Not.Nullable();

            Map(x => x.FirstName)
                .Column("first_name")
                .Not.Nullable();

            Map(x => x.LastName)
                .Column("last_name")
                .Not.Nullable();

            Map(x => x.Email)
                .Column("email")
                .Not.Nullable()
                .Unique();

            Map(x => x.PhoneNumber)
                .Column("phone_number")
                .Not.Nullable()
                .Unique();

            Map(x => x.HiredDate)
                .Column("hired_date")
                .Not.Nullable();

            Map(x => x.LeftDate)
                .Column("left_date")
                .Not.Nullable();

            References(x => x.EmergencyContact)
                .Column("emergency_contact_id")
                .Nullable()
                .Fetch.Join();

            References(x => x.Department)
                .Column("department_id")
                .Not.Nullable()
                .Fetch.Join();

            References(x => x.Location)
                .Column("location_id")
                .Not.Nullable()
                .Fetch.Join();

            References(x => x.Manager)
                .Column("manager_id")
                .Nullable()
                .Fetch.Join();

            References(x => x.Salary)
                .Column("salary_id")
                .Not.Nullable()
                .Fetch.Join();

        }
    }
}
