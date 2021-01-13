using System.Collections.Generic;
using System.Threading.Tasks;

using StolenBikes.Core.Application.Features.StolenBikesForLocation.Queries.GetStolenBikesForLocation;

namespace StolenBikes.WebMvc.Services
{
    public interface IStolenBikesStatisticsService
    {
        Task<GetStolenBikesForLocationViewModel> GetStolenBikesStatisticsForLocation(string location,
            int proximitySquare);

        Task<List<GetStolenBikesForLocationViewModel>> GetStolenBikesStatisticsForCurrentCities(int proximitySquare);

        Task<List<GetStolenBikesForLocationViewModel>> GetStolenBikesStatisticsForFutureCities(int proximitySquare);
    }
}