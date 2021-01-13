using AutoMapper;

using StolenBikes.Core.Application.DTOs;
using StolenBikes.Core.Application.Features.StolenBikesForLocation.Queries.GetStolenBikesForDashboardLocations;
using StolenBikes.Core.Application.Features.StolenBikesForLocation.Queries.GetStolenBikesForLocation;

namespace StolenBikes.Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<StolenBikesForLocationDto, GetStolenBikesForLocationViewModel>().ReverseMap();

            CreateMap<GetStolenBikesForLocationQuery, GetStolenBikesForLocationParameter>();
            CreateMap<GetStolenBikesForDashboardLocationsQuery, GetStolenBikesForDashboardLocationsParameters>();
        }
    }
}
