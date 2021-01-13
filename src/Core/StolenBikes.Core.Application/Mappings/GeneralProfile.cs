using AutoMapper;

using StolenBikes.Core.Application.Features.StolenBikesForLocation.Queries.GetStolenBikesForLocation;
using StolenBikes.Core.Domain.Entities;

namespace StolenBikes.Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<StolenBikeIncident, GetStolenBikesForLocationViewModel>().ReverseMap();
            CreateMap<GetStolenBikesForLocationQuery, GetStolenBikesForLocationParameter>();
        }
    }
}
