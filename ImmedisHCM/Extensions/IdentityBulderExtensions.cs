using ImmedisHCM.Data.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace ImmedisHCM.Web.Extensions
{
    public static class IdentityBulderExtensions
    {
        public static IdentityBuilder AddNHibernateStores(this IdentityBuilder builder)
        {
            var userStoreServiceType = typeof(IUserStore<>)
                    .MakeGenericType(builder.UserType);
            var userStoreImplType = typeof(UserStore<,>)
                .MakeGenericType(builder.UserType, builder.RoleType);

            builder.Services.AddScoped(userStoreServiceType, userStoreImplType);

            var roleStoreSvcType = typeof(IRoleStore<>)
                .MakeGenericType(builder.RoleType);
            var roleStoreImplType = typeof(RoleStore<>)
                .MakeGenericType(builder.RoleType);

            builder.Services.AddScoped(roleStoreSvcType, roleStoreImplType);

            return builder;
        }
    }
}
