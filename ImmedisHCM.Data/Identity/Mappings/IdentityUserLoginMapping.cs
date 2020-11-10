using FluentNHibernate.Mapping;
using ImmedisHCM.Data.Identity.Entities;

namespace ImmedisHCM.Data.Identity.Mappings
{
    public class IdentityUserLoginMapping : ClassMap<WebUserLogin>
    {
        public IdentityUserLoginMapping()
        {
            CompositeId()
                .KeyProperty(x => x.LoginProvider, 
                prop => prop.ColumnName("login_provider")
                        .Length(128)
                        .Type(typeof(string))
                )
                .KeyProperty(x => x.ProviderKey, 
                prop => prop.ColumnName("provider_key")
                        .Length(128)
                        .Type(typeof(string)));

            Map(x => x.ProviderDisplayName)
                .Column("provider_display_name")
                .Length(32);

            Map(x => x.UserId)
                .Column("user_id")
                .Not.Nullable();

            Table("aspnet_user_logins");
        }
    }
}
