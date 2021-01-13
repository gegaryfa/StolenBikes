using System.Threading.Tasks;

using EnsureThat;

using StolenBikes.Core.Application.DTOs;
using StolenBikes.Core.Application.Interfaces.Services;
using StolenBikes.Infrastructure.Shared.Services.StolenBikes.Helpers;

namespace StolenBikes.Infrastructure.Shared.Services.StolenBikes
{
    public class GetStolenBikesForLocationService : IGetStolenBikesForLocationService
    {
        private readonly IStolenBikesDataHelper _stolenBikesDataHelper;

        public GetStolenBikesForLocationService(IStolenBikesDataHelper stolenBikesDataHelper)
        {
            EnsureArg.IsNotNull(stolenBikesDataHelper, nameof(stolenBikesDataHelper));

            _stolenBikesDataHelper = stolenBikesDataHelper;
        }

        public async Task<StolenBikesForLocationDto> GetStolenBikesForLocation(string proximity, int proximitySquare)
        {
            EnsureArg.IsNotNull(proximity, nameof(proximity));
            EnsureArg.IsGt(proximitySquare, 0);

            var allStolenBikesInLocation = await _stolenBikesDataHelper.FetchAllStolenBikes(proximity, proximitySquare);

            var result = new StolenBikesForLocationDto
            {
                Proximity = proximity,
                ProximitySquare = proximitySquare,
                StolenBikesCount = allStolenBikesInLocation.Count
            };

            return result;
        }
    }
}
