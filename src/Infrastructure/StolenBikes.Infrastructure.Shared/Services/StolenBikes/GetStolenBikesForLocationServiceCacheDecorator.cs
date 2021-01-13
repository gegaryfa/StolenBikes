
using System;
using System.Threading.Tasks;

using Microsoft.Extensions.Caching.Memory;

using StolenBikes.Core.Application.DTOs;
using StolenBikes.Core.Application.Interfaces.Services;

namespace StolenBikes.Infrastructure.Shared.Services.StolenBikes
{
    public class GetStolenBikesForLocationServiceCacheDecorator : IGetStolenBikesForLocationService
    {
        private readonly IGetStolenBikesForLocationService _getStolenBikesForLocationService;
        private readonly IMemoryCache _cache;

        public GetStolenBikesForLocationServiceCacheDecorator(IGetStolenBikesForLocationService getStolenBikesForLocationService, IMemoryCache cache)
        {
            _getStolenBikesForLocationService = getStolenBikesForLocationService;
            _cache = cache;
        }

        public async Task<StolenBikesForLocationDto> GetStolenBikesForLocation(string proximity, int proximitySquare)
        {
            var cacheKey = $"{proximity}{proximitySquare}";

            Func<ICacheEntry, Task<StolenBikesForLocationDto>> factory = async cacheEntry => await _getStolenBikesForLocationService.GetStolenBikesForLocation(proximity, proximitySquare);

            var cachedProperties = await _cache
                .GetOrCreateAsync(cacheKey, factory);

            return cachedProperties;
        }
    }
}
