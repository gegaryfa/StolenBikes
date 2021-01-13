using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Newtonsoft.Json;

using RestEase;

using StolenBikes.Core.Domain.Entities;
using StolenBikes.Infrastructure.Shared.Services.StolenBikes.Clients;

namespace StolenBikes.Infrastructure.Shared.Services.StolenBikes.Helpers
{
    public class StolenBikesDataHelper : IStolenBikesDataHelper
    {

        private const int PageSize = 2500;
        private readonly IStolenBikesApi _stolenBikesApi;

        public StolenBikesDataHelper(IStolenBikesApi stolenBikesApi)
        {
            this._stolenBikesApi = stolenBikesApi;
        }

        public async Task<List<StolenBikeIncident>> FetchAllStolenBikes(string proximity, int proximitySquare)
        {
            var totalObjects = new List<StolenBikeIncident>();

            // Get the first page in order to find the total number of pages
            var firstApiResponseContent = await GetApiResponseContentForPageAsync(1, PageSize, proximity, proximitySquare);

            // Add the objects of the current page to the total amount of objects
            totalObjects.AddRange(firstApiResponseContent.Incidents);

            // 16/15 = 0, that should lead to 2 pages (hence the +2) 
            var totalPagesCount = firstApiResponseContent.Paging.TotalObjects / PageSize + 2;

            var totalPages = Enumerable.Range(2, totalPagesCount).ToList();

            if (totalPages.Count == 1)
            {
                return totalObjects;
            }

            // Setup the rest of the tasks
            var getApiResponseTaskQuery = totalPages.Select(page =>
                GetApiResponseContentForPageAsync(page, PageSize, proximity, proximitySquare));

            // Use ToList to execute the query and start the tasks.
            var apiResponseTasks = getApiResponseTaskQuery.ToList();

            // Process the tasks one at a time until none remain.
            while (apiResponseTasks.Count > 0)
            {
                // Identify the first task that completes.
                var finishedTask = await Task.WhenAny(apiResponseTasks);

                // Remove the selected task from the list so that you don't process it more than once.
                apiResponseTasks.Remove(finishedTask);

                // Await the completed task.
                var apiResponseContent = await finishedTask;

                // Add the object of the page in the total objects
                totalObjects.AddRange(apiResponseContent.Incidents);
            }

            return totalObjects;
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
                Console.WriteLine(ex);
                throw;
            }
        }

    }
}
