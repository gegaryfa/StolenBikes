using System.Reflection;

using AutoMapper;


using MediatR;

using Microsoft.Extensions.DependencyInjection;

namespace StolenBikes.Core.Application
{
    public static class ServiceExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
        }
    }
}
