using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using EnsureThat;

using MediatR;

using Microsoft.Extensions.Options;

using StolenBikes.Core.Application.Features.StolenBikesForLocation.Queries.GetStolenBikesForLocation;
using StolenBikes.Core.Application.Interfaces.Services;
using StolenBikes.Core.Application.Settings;

namespace StolenBikes.Core.Application.Features.StolenBikesForLocation.Queries.GetStolenBikesForDashboardLocations
{
    public class GetStolenBikesForDashboardLocationsQuery : IRequest<GetStolenBikesForDashboardLocationsViewModel>
    {
        public int ProximitySquare { get; set; }
    }

    public class GetStolenBikesForDashboardLocationsQueryHandler : IRequestHandler<GetStolenBikesForDashboardLocationsQuery, GetStolenBikesForDashboardLocationsViewModel>
    {
        private readonly IGetStolenBikesForLocationService _getStolenBikesForLocationService;
        private readonly IMapper _mapper;

        private readonly string[] _currentLocations;
        private readonly string[] _futureLocations;
        public GetStolenBikesForDashboardLocationsQueryHandler(IGetStolenBikesForLocationService getStolenBikesForLocationService, IMapper mapper, IOptions<DashboardLocationsSettings> locationsStatisticsSettings)
        {
            EnsureArg.IsNotNull(getStolenBikesForLocationService, nameof(getStolenBikesForLocationService));
            EnsureArg.IsNotNull(mapper, nameof(mapper));

            _getStolenBikesForLocationService = getStolenBikesForLocationService;
            _mapper = mapper;

            var locationsSettings = locationsStatisticsSettings.Value;
            _currentLocations = locationsSettings.CurrentLocations;
            _futureLocations = locationsSettings.FutureLocations;
        }

        public async Task<GetStolenBikesForDashboardLocationsViewModel> Handle(GetStolenBikesForDashboardLocationsQuery query, CancellationToken cancellationToken)
        {
            // We can do some validation before the mapping
            var request = _mapper.Map<GetStolenBikesForDashboardLocationsParameters>(query);

            var bikeTheftsInCurrentCitiesTask = GetStolenBikesStatisticsForCurrentCities(request.ProximitySquare);
            var bikeTheftsInFutureCitiesTask = GetStolenBikesStatisticsForFutureCities(request.ProximitySquare);

            var response = new GetStolenBikesForDashboardLocationsViewModel
            {
                StolenBikesInCurrentCities = await bikeTheftsInCurrentCitiesTask,
                StolenBikesInFutureCities = await bikeTheftsInFutureCitiesTask
            };

            return response;
        }

        private async Task<List<GetStolenBikesForLocationViewModel>> GetStolenBikesStatisticsForCurrentCities(int proximitySquare)
        {
            return await this.GetStolenBikesStatisticsForMultipleLocations(_currentLocations, proximitySquare);
        }

        private async Task<List<GetStolenBikesForLocationViewModel>> GetStolenBikesStatisticsForFutureCities(int proximitySquare)
        {
            return await this.GetStolenBikesStatisticsForMultipleLocations(_futureLocations, proximitySquare);
        }

        private async Task<List<GetStolenBikesForLocationViewModel>> GetStolenBikesStatisticsForMultipleLocations(string[] locations, int proximitySquare)
        {
            var stolenBikesInLocations = new List<GetStolenBikesForLocationViewModel>();

            var stolenBikesInLocationTasks = locations
                .Select(city =>
                    _getStolenBikesForLocationService.GetStolenBikesForLocation(city, proximitySquare)
                   )
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


                stolenBikesInLocations.Add(_mapper.Map<GetStolenBikesForLocationViewModel>(apiResponseContent));
            }

            var sortedLocations = stolenBikesInLocations.OrderBy(l => l.StolenBikesCount);
            return sortedLocations.ToList();
        }
    }
}
