using FluentNHibernate.Mapping;

namespace ImmedisHCM.Data.Entities.Mappings
{
    public class CurrencyMapping : ClassMap<Currency>
    {
        public CurrencyMapping()
        {
            Table("currency");

            Id(x => x.Id)
                .Column("id")
                .GeneratedBy.Increment();

            Map(x => x.Name)
                .Column("name")
                .Not.Nullable();

            HasMany(x => x.Salaries)
                .Inverse()
                .KeyColumn("currency_id")
                .Fetch.Select();
        }
    }
}
