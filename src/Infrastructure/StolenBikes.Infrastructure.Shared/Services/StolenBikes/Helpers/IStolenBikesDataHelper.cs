using System.Collections.Generic;
using System.Threading.Tasks;

using StolenBikes.Core.Domain.Entities;

namespace StolenBikes.Infrastructure.Shared.Services.StolenBikes.Helpers
{
    public interface IStolenBikesDataHelper
    {
        /// <summary>
        /// Fetches all stolen bikes within an area.
        /// </summary>
        /// <param name="proximity">
        /// Accepts an ip address, an address, zipcode, city, or latitude,longitude
        /// - i.e. 70.210.133.87, 210 NW 11th Ave, Portland, OR, 60647, Chicago, IL,
        /// and 45.521728,-122.67326 are all acceptable
        /// </param>
        /// <param name="proximitySquare">
        /// Sets the length of the sides of the square to find matches inside of.
        /// The square is centered on the location specified by proximity.
        /// </param>
        /// <returns>A list of stolen.</returns>
        Task<List<StolenBikeIncident>> FetchAllStolenBikes(string proximity, int proximitySquare);
    }
}