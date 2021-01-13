using System.Diagnostics;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using StolenBikes.Core.Application.Features.StolenBikesForLocation.Queries.GetStolenBikesForDashboardLocations;
using StolenBikes.Core.Application.Features.StolenBikesForLocation.Queries.GetStolenBikesForLocation;
using StolenBikes.WebMvc.Models;

namespace StolenBikes.WebMvc.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var defaultProximitySquare = 10;
            var viewModel = await Mediator.Send(new GetStolenBikesForDashboardLocationsQuery()
            {
                ProximitySquare = defaultProximitySquare
            });

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(int proximitySquare)
        {
            var viewModel = await Mediator.Send(new GetStolenBikesForDashboardLocationsQuery()
            {
                ProximitySquare = proximitySquare
            });

            return View(viewModel);
        }



        public IActionResult CustomLocation()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CustomLocation(string location, int proximitySquare)
        {
            var viewModel = await Mediator.Send(new GetStolenBikesForLocationQuery
            {
                Proximity = location,
                ProximitySquare = proximitySquare
            });

            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
