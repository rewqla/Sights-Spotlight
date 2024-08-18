using API.Contract.Requests.Sight;
using API.Contract.Responses.Country;
using API.Routes;
using Microsoft.AspNetCore.Mvc;
using StoreBLL.Interfaces;

namespace API.Controllers
{
    [ApiController]
    public class SightController : ControllerBase
    {
        private readonly ISightService _sightsService;
        public SightController(ISightService sightsService)
        {
            _sightsService = sightsService;
        }

        [HttpGet(SightRoutes.GetAll)]
        public async Task<ActionResult<IAsyncEnumerable<CountryResponse>>> GetSights([FromQuery] GetAllSightsRequest request, CancellationToken cancellationToken)
        {
            var sights = await _sightsService.GetAllSights(request, cancellationToken);

            return Ok(sights);
        }
    }
}
