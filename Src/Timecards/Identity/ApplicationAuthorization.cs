using Microsoft.Extensions.DependencyInjection;

namespace Timecards.Identity
{
    public static class ApplicationAuthorization
    {
        public const string HasAdminPermission = "HasViewUserPermission";

        public static void AddApplicationAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(HasAdminPermission, policy => policy.RequireRole("Admin"));
            });
        }
    }
}