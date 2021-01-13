using System.Collections.Generic;
using System.Threading.Tasks;

using StolenBikes.Core.Domain.Entities;

namespace StolenBikes.Core.Application.Interfaces.Services
{
    public interface IGetStolenBikesForLocationService
    {
        Task<IEnumerable<StolenBikeIncident>> GetStolenBikesForLocation(string proximity, int proximitySquare);
    }
}
