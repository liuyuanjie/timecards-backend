using Microsoft.Extensions.DependencyInjection;

namespace Timecards.Identity
{
    public static class ApplicationAuthorization
    {
        public const string HasViewUserPermission = "HasViewUserPermission";

        public static void AddApplicationAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(HasViewUserPermission, policy => policy.RequireRole("Admin"));
            });
        }
    }
}