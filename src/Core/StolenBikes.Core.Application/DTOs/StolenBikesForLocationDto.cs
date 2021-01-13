namespace StolenBikes.Core.Application.DTOs
{
    public class StolenBikesForLocationDto
    {
        public string Proximity { get; set; }
        public int ProximitySquare { get; set; }
        public int StolenBikesCount { get; set; }
    }
}
