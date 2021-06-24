using Microsoft.Extensions.DependencyInjection;
using SwtchApi.Services;

namespace SwtchApi.Extensions
{
    public static class ApiServiceExtensions
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddTransient<IColorService, ColorService>();

            return services;
        }
    }
}