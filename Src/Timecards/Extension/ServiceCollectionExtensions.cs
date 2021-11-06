using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Timecards.Application.Interfaces;
using Timecards.Common;
using Timecards.Domain;
using Timecards.Infrastructure.EF;

namespace Timecards.Extension
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, TimecardsDbContext>();
            services.AddScoped<IDatabaseInitializer, DatabaseInitializer>();
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
            services.Configure<ConnectionOptions>(AppSettings.Current.GetSection(ConnectionOptions.Position));
            services.AddScoped<IConnection, MSSqlConnection>();

            return services;
        }

        public static void AddTimecardsIdentity(this IServiceCollection services)
        {
            services.AddIdentity<Account, IdentityRole<Guid>>(setup =>
                {
                    setup.Password.RequireDigit = false;
                    setup.Password.RequireLowercase = false;
                    setup.Password.RequireNonAlphanumeric = false;
                    setup.Password.RequiredLength = 4;
                })
                .AddEntityFrameworkStores<TimecardsDbContext>()
                .AddDefaultTokenProviders();
        }
    }
}