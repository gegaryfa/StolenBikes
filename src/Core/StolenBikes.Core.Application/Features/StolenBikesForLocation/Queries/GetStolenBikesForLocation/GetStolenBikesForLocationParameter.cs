using StolenBikes.Core.Application.Parameters;

namespace StolenBikes.Core.Application.Features.StolenBikesForLocation.Queries.GetStolenBikesForLocation
{
    public class GetStolenBikesForLocationParameter : RequestParameter
    {
        public string Proximity { get; set; }

        public int ProximitySquare { get; set; }
    }
}
