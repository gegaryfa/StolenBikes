using System.Reflection;

using AutoMapper;


using MediatR;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using StolenBikes.Core.Application.Settings;

namespace StolenBikes.Core.Application
{
    public static class ServiceExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DashboardLocationsSettings>(configuration.GetSection("DashboardLocationsSettings"));

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
        }
    }
}
