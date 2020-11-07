using FluentNHibernate.Mapping;
using ImmedisHCM.Data.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImmedisHCM.Data.Identity.Mappings
{
    class IdentityUserRoleMapping : ClassMap<WebUserRole>
    {
        public IdentityUserRoleMapping()
        {
            CompositeId()
                .KeyProperty(x => x.UserId,
                prop => prop.ColumnName("user_id")
                        .Type(typeof(Guid)))
                .KeyProperty(x => x.RoleId,
                prop => prop.ColumnName("role_id")
                        .Type(typeof(Guid)));

            Table("aspnet_user_roles");
        }
    }
}
