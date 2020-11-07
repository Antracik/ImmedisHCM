using FluentNHibernate.Mapping;
using ImmedisHCM.Data.Identity.Entities;
using NHibernate.Type;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ImmedisHCM.Data.Identity.Mappings
{
    public class IdentityRoleClaimMapping : ClassMap<WebRoleClaim>
    {
        public IdentityRoleClaimMapping()
        {
            Id(x => x.Id)
                .Column("id")
                .GeneratedBy.Increment();

            Map(x => x.ClaimType)
                .Column("claim_type");

            Map(x => x.ClaimValue)
                .Column("claim_value");

            References<WebRole>(x => x.RoleId)
                .Column("role_id")
                .Not.Nullable()
                .ForeignKey("id")
                .Cascade.All()
                .Index("ix_aspnet_role_claims_role_id");

            Table("aspnet_role_claims");
        }
    }
}
