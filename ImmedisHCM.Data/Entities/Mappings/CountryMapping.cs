using FluentNHibernate.Mapping;

namespace ImmedisHCM.Data.Entities.Mappings
{
    public class CountryMapping : ClassMap<Country>
    {
        public CountryMapping()
        {
            Table("countries");
            Id(x => x.Id)
                .GeneratedBy.GuidComb();

            Map(x => x.Name)
                .Column("name")
                .Not.Nullable();

            Map(x => x.ShortName)
                .Column("short_name")
                .Not.Nullable();

            HasMany(x => x.Cities)
                .KeyColumn("country_id")
                .Inverse()
                .Fetch.Join();
        }
    }
}
