
using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;

using StolenBikes.Core.Application.Interfaces.Services;
using StolenBikes.Core.Domain.Entities;
using StolenBikes.Infrastructure.Shared.Services.StolenBikes.Helpers;

namespace StolenBikes.Infrastructure.Shared.Services.StolenBikes
{
    public class GetStolenBikesForLocationService : IGetStolenBikesForLocationService
    {
        private readonly IStolenBikesDataHelper _stolenBikesDataHelper;
        private readonly IMapper _mapper;

        public GetStolenBikesForLocationService(IStolenBikesDataHelper stolenBikesDataHelper, IMapper mapper)
        {
            _stolenBikesDataHelper = stolenBikesDataHelper;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StolenBikeIncident>> GetStolenBikesForLocation(string proximity, int proximitySquare)
        {
            var allStolenBikesInLocation = await _stolenBikesDataHelper.FetchAllStolenBikes(proximity, proximitySquare);

            var result = _mapper.Map<IEnumerable<StolenBikeIncident>>(allStolenBikesInLocation);
            return result;
        }
    }
}
