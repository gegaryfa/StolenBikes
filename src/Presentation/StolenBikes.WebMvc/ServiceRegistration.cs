
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using StolenBikes.WebMvc.Services;
using StolenBikes.WebMvc.Settings;

namespace StolenBikes.WebMvc
{
    public static class ServiceRegistration
    {
        public static void PresentationLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<LocationsStatisticsSettings>(configuration.GetSection("LocationsStatisticsSettings"));

            services.AddTransient<IStolenBikesStatisticsService, StolenBikesStatisticsService>();
        }
    }
}
