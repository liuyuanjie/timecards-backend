using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Timecards.Application.Interfaces;

namespace Timecards.Extension
{
    public static class HostExtensions
    {
        public static void MigrateDbContext<TContext>(this IWebHost host) where TContext : DbContext
        {
            using (var scope = host.Services.CreateScope())
            {
                MigrationDbContext<TContext>(scope);
            }
        }

        public static void MigrateDbContext<TContext>(this IHost host) where TContext : DbContext
        {
            using (var scope = host.Services.CreateScope())
            {
                MigrationDbContext<TContext>(scope);
            }
        }

        private static void MigrationDbContext<TContext>(IServiceScope scope) where TContext : DbContext
        {
            var services = scope.ServiceProvider;

            try
            {
                var context = services.GetRequiredService<TContext>();
                context.Database.Migrate();

                var initializer = services.GetRequiredService<IDatabaseInitializer>();
                initializer.Seed().Wait();
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred seeding the DB.");
            }
        }
    }
}