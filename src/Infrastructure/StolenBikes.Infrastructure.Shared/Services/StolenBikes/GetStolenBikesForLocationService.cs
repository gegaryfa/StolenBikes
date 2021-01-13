using System.Linq;
using System.Threading.Tasks;

using EnsureThat;

using Newtonsoft.Json;

using RestEase;

using StolenBikes.Core.Application.DTOs;
using StolenBikes.Core.Application.Interfaces.Services;
using StolenBikes.Infrastructure.Shared.Services.StolenBikes.Clients;

namespace StolenBikes.Infrastructure.Shared.Services.StolenBikes
{
    public class GetStolenBikesForLocationService : IGetStolenBikesForLocationService
    {
        private const int CurrentPage = 1;
        private const int PageSize = 1;
        private readonly IStolenBikesApi _stolenBikesApi;

        public GetStolenBikesForLocationService(IStolenBikesApi stolenBikesApi)
        {
            EnsureArg.IsNotNull(stolenBikesApi, nameof(stolenBikesApi));

            _stolenBikesApi = stolenBikesApi;
        }

        public async Task<StolenBikesForLocationDto> GetStolenBikesForLocation(string proximity, int proximitySquare)
        {
            EnsureArg.IsNotNull(proximity, nameof(proximity));
            EnsureArg.IsGt(proximitySquare, 0);

            var allStolenBikesInLocation = await GetApiResponseContentForPageAsync(CurrentPage, PageSize, proximity, proximitySquare);

            var result = new StolenBikesForLocationDto
            {
                Proximity = proximity,
                ProximitySquare = proximitySquare,
                StolenBikesCount = allStolenBikesInLocation.Paging.TotalObjects
            };

            return result;
        }


        private async Task<StolenBikesApiResponse> GetApiResponseContentForPageAsync(int currentPage, int pageSize, string proximity, int proximitySquare)
        {
            try
            {
                var apiResponse = await _stolenBikesApi.GetStolenBikesForLocationAsync(currentPage, pageSize, proximity, proximitySquare);

                if (!apiResponse.ResponseMessage.IsSuccessStatusCode)
                {
                    var errorResult = JsonConvert.DeserializeObject<ApiException>(apiResponse.StringContent ?? string.Empty);
                    throw errorResult;
                }

                var result = apiResponse.GetContent();

                result.Paging.TotalObjects = int.Parse(apiResponse.ResponseMessage.Headers.GetValues("total").First());
                return result;
            }
            catch (ApiException ex)
            {
                //Log it
                throw;
            }
        }
    }
}
