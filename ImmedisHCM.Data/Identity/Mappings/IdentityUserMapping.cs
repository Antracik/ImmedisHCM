using FluentNHibernate.Mapping;
using ImmedisHCM.Data.Identity.Entities;

namespace ImmedisHCM.Data.Identity.Mappings
{
    public class IdentityUserMapping : ClassMap<WebUser>
    {
        public IdentityUserMapping()
        {
            Id(x => x.Id)
                .Column("id")
                .GeneratedBy.GuidComb();

            Map(x => x.UserName)
                .Column("user_name")
                .Length(256)
                .Unique();

            Map(x => x.NormalizedUserName)
                .Column("normalized_user_name")
                .Length(256)
                .Unique()
                .Index("ix_aspnet_users_user_name");

            Map(x => x.Email)
                .Column("email")
                .Length(256);

            Map(x => x.NormalizedEmail)
                .Column("normalized_email")
                .Length(256)
                .Index("ix_aspnet_users_email");

            Map(x => x.EmailConfirmed)
                .Column("email_confirmed")
                .Not.Nullable();

            Map(x => x.PhoneNumber)
                .Column("phone_number");

            Map(x => x.PhoneNumberConfirmed)
                .Column("phone_number_confirmed")
                .Not.Nullable();

            Map(x => x.LockoutEnabled)
                .Column("lockout_enabled")
                .Not.Nullable();

            Map(x => x.LockoutEndUnixTimeSeconds)
                .Column("lockout_end_unix_time_seconds");

            Map(x => x.AccessFailedCount)
                .Column("access_failed_count")
                .Not.Nullable();

            Map(x => x.PasswordHash)
                .Column("password_hash");

            Map(x => x.SecurityStamp)
                .Column("security_stamp")
                .Length(256);

            Map(x => x.TwoFactorEnabled)
                .Column("two_factor_enabled")
                .Not.Nullable();

            Map(x => x.ConcurrencyStamp)
                .Column("concurrency_stamp")
                .Length(36);


            Table("aspnet_users");
        }
    }
}
