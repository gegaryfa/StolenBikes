using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using RestEase;

using StolenBikes.Core.Application.Interfaces.Services;
using StolenBikes.Infrastructure.Shared.Services.StolenBikes;
using StolenBikes.Infrastructure.Shared.Services.StolenBikes.Clients;
using StolenBikes.Infrastructure.Shared.Services.StolenBikes.Helpers;

namespace StolenBikes.Infrastructure.Shared
{
    public static class ServiceRegistration
    {
        public static void AddSharedInfrastructure(this IServiceCollection services, IConfiguration config)
        {

            services.AddTransient<IStolenBikesDataHelper, StolenBikesDataHelper>();

            services.AddTransient<IGetStolenBikesForLocationService, GetStolenBikesForLocationService>();

            services.AddSingleton(
                serviceProvider =>
                {
                    var basePath = config["StolenBikesApi:basePath"];
                    var client = RestClient.For<IStolenBikesApi>(basePath);
                    return client;
                });
        }
    }
}
