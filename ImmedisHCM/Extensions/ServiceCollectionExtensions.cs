﻿using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using ImmedisHCM.Data.Identity.Entities;
using ImmedisHCM.Web.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace ImmedisHCM.Web.Extensions
{
    public static class ServiceCollectionExtensions
    {
        
        public static IServiceCollection AddNHibernateIdentity(this IServiceCollection services)
        {
            services.AddDefaultIdentity<WebUser>(opts =>
            {
                opts.User.RequireUniqueEmail = true;
            }).AddRoles<WebRole>()
            .AddNHibernateStores();

            return services;
        }

        public static IServiceCollection AddNHibernateSessionFactory(this IServiceCollection services, string connectionString)
        {
            var sessionFactory = Fluently.Configure()
                                    .Database(PostgreSQLConfiguration.PostgreSQL82
                                    .ConnectionString(connectionString)
                                    .FormatSql()
                                    .ShowSql())
                                    .Mappings(x => x.FluentMappings.AddFromAssemblyOf<WebUser>())
                                    .ExposeConfiguration(x => x.SetInterceptor(new SqlStatementInterceptor()))
                                    .BuildSessionFactory();

            services.AddSingleton(sessionFactory);
            services.AddScoped(factory => sessionFactory.OpenSession());

            return services;
        }
    }
}
