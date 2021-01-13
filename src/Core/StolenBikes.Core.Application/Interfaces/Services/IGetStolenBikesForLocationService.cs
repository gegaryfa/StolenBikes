using System.Threading.Tasks;

using StolenBikes.Core.Application.DTOs;

namespace StolenBikes.Core.Application.Interfaces.Services
{
    public interface IGetStolenBikesForLocationService
    {
        Task<StolenBikesForLocationDto> GetStolenBikesForLocation(string proximity, int proximitySquare);
    }
}
