using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using StolenBikes.Core.Application.Features.StolenBikesForLocation.Queries.GetStolenBikesForLocation;
using StolenBikes.Core.Application.Interfaces.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StolenBikes.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class StolenBikesController : BaseApiController
    {

        private readonly IGetStolenBikesForLocationService _stolenBikesForLocationService;

        public StolenBikesController(IGetStolenBikesForLocationService stolenBikesForLocationService)
        {
            _stolenBikesForLocationService = stolenBikesForLocationService;
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetStolenBikesForLocationParameter filter)
        {
            var res = await _stolenBikesForLocationService.GetStolenBikesForLocation("Amsterdam", 2000);

            return Ok(res);
        }

        //// GET: api/<controller>
        //[HttpGet]
        //public async Task<IActionResult> Get([FromQuery] GetStolenBikesForLocationParameter filter)
        //{

        //    return Ok(await Mediator.Send(new GetStolenBikesForLocationQuery() { }));
        //}
    }
}
