using FluentNHibernate.Mapping;
using ImmedisHCM.Data.Identity.Entities;
using System;

namespace ImmedisHCM.Data.Identity.Mappings
{
    public class IdentityUserTokenMapping : ClassMap<WebUserToken>
    {
        public IdentityUserTokenMapping()
        {
            CompositeId()
                .KeyProperty(x => x.UserId,
                prop => prop.ColumnName("user_id")
                        .Type(typeof(Guid)))
                .KeyProperty(x => x.LoginProvider,
                prop => prop.ColumnName("login_provider")
                        .Length(128)
                        .Type(typeof(string)))
                .KeyProperty(x => x.Name,
                prop => prop.ColumnName("name")
                        .Length(128)
                        .Type(typeof(string)));

            Map(x => x.Value)
                .Column("value");

            Table("aspnet_user_tokens");
        }
    }
}
