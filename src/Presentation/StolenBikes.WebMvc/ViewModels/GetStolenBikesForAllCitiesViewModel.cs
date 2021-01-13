
using System.Collections.Generic;

using StolenBikes.Core.Application.Features.StolenBikesForLocation.Queries.GetStolenBikesForLocation;

namespace StolenBikes.WebMvc.ViewModels
{
    public class GetStolenBikesForAllCitiesViewModel
    {
        public GetStolenBikesForAllCitiesViewModel()
        {
            StolenBikesInCurrentCities = new List<GetStolenBikesForLocationViewModel>();
            StolenBikesInFutureCities = new List<GetStolenBikesForLocationViewModel>();
        }

        public List<GetStolenBikesForLocationViewModel> StolenBikesInCurrentCities { get; set; }
        public List<GetStolenBikesForLocationViewModel> StolenBikesInFutureCities { get; set; }
    }
}
