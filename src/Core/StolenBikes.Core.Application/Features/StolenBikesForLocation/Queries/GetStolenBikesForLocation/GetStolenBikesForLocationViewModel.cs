namespace StolenBikes.Core.Application.Features.StolenBikesForLocation.Queries.GetStolenBikesForLocation
{
    public class GetStolenBikesForLocationViewModel
    {
        public string Proximity { get; set; }

        public int ProximitySquare { get; set; }
        public int StolenBikesCount { get; set; }
    }
}
