using FluentNHibernate.Mapping;

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
                .Nullable();

            References(x => x.Job)
                .Column("job_id")
                .Not.Nullable();

            References(x => x.EmergencyContact)
                .Column("emergency_contact_id")
                .Nullable();

            References(x => x.Department)
                .Column("department_id")
                .Not.Nullable();

            References(x => x.Location)
                .Column("location_id")
                .Not.Nullable();

            References(x => x.Manager)
                .Column("manager_id")
                .Nullable();

            References(x => x.Salary)
                .Column("salary_id")
                .Not.Nullable();

        }
    }
}
