using System.Diagnostics;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using StolenBikes.WebMvc.Models;
using StolenBikes.WebMvc.Services;
using StolenBikes.WebMvc.ViewModels;

namespace StolenBikes.WebMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStolenBikesStatisticsService _stolenBikesStatisticsService;

        public HomeController(ILogger<HomeController> logger, IStolenBikesStatisticsService stolenBikesStatisticsService)
        {
            _logger = logger;
            _stolenBikesStatisticsService = stolenBikesStatisticsService;
        }

        public async Task<IActionResult> Index()
        {
            var defaultProximitySquare = 10;
            var viewModel = await GetStolenBikesForAllCities(defaultProximitySquare);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(int proximitySquare)
        {
            var viewModel = await GetStolenBikesForAllCities(proximitySquare);

            return View(viewModel);
        }

        private async Task<GetStolenBikesForAllCitiesViewModel> GetStolenBikesForAllCities(int proximitySquare)
        {
            var currentCitiesStolenBikes =
                _stolenBikesStatisticsService.GetStolenBikesStatisticsForCurrentCities(proximitySquare);

            var futureCitiesStolenBikes =
                _stolenBikesStatisticsService.GetStolenBikesStatisticsForFutureCities(proximitySquare);

            var viewModel = new GetStolenBikesForAllCitiesViewModel();
            viewModel.StolenBikesInCurrentCities.AddRange(await currentCitiesStolenBikes);
            viewModel.StolenBikesInFutureCities.AddRange(await futureCitiesStolenBikes);
            return viewModel;
        }


        public IActionResult CustomLocation()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CustomLocation(string location, int proximitySquare)
        {
            var viewModel = await _stolenBikesStatisticsService.GetStolenBikesStatisticsForLocation(location, proximitySquare);

            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
