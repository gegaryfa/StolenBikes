namespace StolenBikes.Core.Application.Features.StolenBikesForLocation.Queries.GetStolenBikesForLocation
{
    public class GetStolenBikesForLocationParameter
    {
        public string Proximity { get; set; }

        public int ProximitySquare { get; set; }
    }
}
