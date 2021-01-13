using System.Threading.Tasks;

using RestEase;

namespace StolenBikes.Infrastructure.Shared.Services.StolenBikes.Clients
{
    /// <summary>
    /// Interface for the stolen bikes client.
    /// </summary>
    public interface IStolenBikesApi
    {
        /// <summary>
        /// Gets the registered incidents from the v2/incidents endpoint.
        /// </summary>
        /// <param name="page">Page number</param>
        /// <param name="per_page">Results per page.</param>
        /// <param name="proximity">
        /// The location to search for stolen bikes.
        /// Accepts an ip address, an address, zipcode, city, or latitude,longitude
        /// - i.e. 70.210.133.87, 210 NW 11th Ave, Portland, OR, 60647, Chicago, IL,
        /// and 45.521728,-122.67326 are all acceptable
        /// </param>
        /// <param name="proximity_square">
        /// Sets the length of the sides of the square to find matches inside of.
        /// The square is centered on the location specified by proximity.
        /// </param>
        /// <returns></returns>
        [Get("api/v2/incidents")]
        Task<Response<StolenBikesApiResponse>> GetStolenBikesForLocationAsync([Query] int page, [Query] int per_page, [Query] string proximity, [Query] int proximity_square);
    }
}
