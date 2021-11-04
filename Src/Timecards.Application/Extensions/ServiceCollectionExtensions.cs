using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR;

namespace Timecards.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
        }
    }
}