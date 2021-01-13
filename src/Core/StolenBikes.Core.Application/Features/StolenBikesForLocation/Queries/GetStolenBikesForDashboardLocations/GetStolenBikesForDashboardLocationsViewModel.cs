using System.Collections.Generic;

using StolenBikes.Core.Application.Features.StolenBikesForLocation.Queries.GetStolenBikesForLocation;

namespace StolenBikes.Core.Application.Features.StolenBikesForLocation.Queries.GetStolenBikesForDashboardLocations
{
    public class GetStolenBikesForDashboardLocationsViewModel
    {
        public GetStolenBikesForDashboardLocationsViewModel()
        {
            StolenBikesInCurrentCities = new List<GetStolenBikesForLocationViewModel>();
            StolenBikesInFutureCities = new List<GetStolenBikesForLocationViewModel>();
        }

        public List<GetStolenBikesForLocationViewModel> StolenBikesInCurrentCities { get; set; }
        public List<GetStolenBikesForLocationViewModel> StolenBikesInFutureCities { get; set; }
    }
}
