using FluentNHibernate.Mapping;
using ImmedisHCM.Data.Identity.Entities;

namespace ImmedisHCM.Data.Identity.Mappings
{
    public class IdentityRoleMapping : ClassMap<WebRole>
    {
        public IdentityRoleMapping()
        {
            Id(x => x.Id)
                .Column("id")
                .GeneratedBy.GuidComb();

            Map(x => x.Name)
                .Column("name")
                .Length(256)
                .Not.Nullable()
                .Unique();

            Map(x => x.NormalizedName)
                .Column("normalized_name")
                .Length(256)
                .Not.Nullable()
                .Unique()
                .Index("ix_aspnet_roles_name");

            Map(x => x.ConcurrencyStamp)
                .Column("concurrency_stamp");

            Table("aspnet_roles");
        }
    }
}
