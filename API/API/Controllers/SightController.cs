using API.Contract.Responses;
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
        public async Task<ActionResult<IAsyncEnumerable<CountryResponse>>> GetSights(CancellationToken cancellationToken)
        {
            var sights = await _sightsService.GetAllSights(cancellationToken);

            return Ok(sights);
        }
    }
}
