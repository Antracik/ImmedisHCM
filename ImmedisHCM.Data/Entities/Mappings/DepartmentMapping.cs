using FluentNHibernate.Mapping;

namespace ImmedisHCM.Data.Entities.Mappings
{
    public class DepartmentMapping : ClassMap<Department>
    {
        public DepartmentMapping()
        {
            Table("departments");
            Id(x => x.Id)
                .Column("id")
                .GeneratedBy.GuidComb();

            Map(x => x.Name)
                .Column("name")
                .Not.Nullable();

            References(x => x.Manager)
                .Column("manager_id")
                .Nullable()
                .Fetch.Join();

            References(x => x.Location)
                .Column("location_id")
                .Not.Nullable()
                .Fetch.Join();

            References(x => x.Company)
                .Column("company_id")
                .Not.Nullable()
                .Fetch.Join();

            HasMany(x => x.Employees)
                .KeyColumn("department_id")
                .Inverse()
                .Fetch.Select();
        }
    }
}
