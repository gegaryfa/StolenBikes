using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Options;

using StolenBikes.Core.Application.Features.StolenBikesForLocation.Queries.GetStolenBikesForLocation;
using StolenBikes.WebMvc.Settings;

namespace StolenBikes.WebMvc.Services
{
    public class StolenBikesStatisticsService : IStolenBikesStatisticsService
    {
        private readonly IMediator _mediator;

        private readonly string[] _currentLocations;
        private readonly string[] _futureLocations;

        public StolenBikesStatisticsService(IMediator mediator, IOptions<LocationsStatisticsSettings> locationsStatisticsSettings)
        {
            _mediator = mediator;

            var locationsSettings = locationsStatisticsSettings.Value;
            _currentLocations = locationsSettings.CurrentLocations;
            _futureLocations = locationsSettings.FutureLocations;
        }

        public Task<GetStolenBikesForLocationViewModel> GetStolenBikesStatisticsForLocation(string location, int proximitySquare)
        {
            return _mediator.Send(new GetStolenBikesForLocationQuery
            {
                Proximity = location,
                ProximitySquare = proximitySquare
            });
        }

        public async Task<List<GetStolenBikesForLocationViewModel>> GetStolenBikesStatisticsForCurrentCities(int proximitySquare)
        {
            return await this.GetStolenBikesStatisticsForMultipleLocations(_currentLocations, proximitySquare);
        }

        public async Task<List<GetStolenBikesForLocationViewModel>> GetStolenBikesStatisticsForFutureCities(int proximitySquare)
        {
            return await this.GetStolenBikesStatisticsForMultipleLocations(_futureLocations, proximitySquare);
        }

        private async Task<List<GetStolenBikesForLocationViewModel>> GetStolenBikesStatisticsForMultipleLocations(string[] locations, int proximitySquare)
        {
            var stolenBikesInLocations = new List<GetStolenBikesForLocationViewModel>();

            var stolenBikesInLocationTasks = locations
                .Select(city =>
                _mediator.Send(new GetStolenBikesForLocationQuery
                {
                    Proximity = city,
                    ProximitySquare = proximitySquare
                }))
                .ToList();// Use ToList to execute the query and start the tasks.

            // Process the tasks one at a time until none remain.
            while (stolenBikesInLocationTasks.Count > 0)
            {
                // Identify the first task that completes.
                var finishedTask = await Task.WhenAny(stolenBikesInLocationTasks);

                // Remove the selected task from the list so that you don't process it more than once.
                stolenBikesInLocationTasks.Remove(finishedTask);

                // Await the completed task.
                var apiResponseContent = await finishedTask;
                stolenBikesInLocations.Add(apiResponseContent);
            }

            return stolenBikesInLocations;
        }
    }
}
