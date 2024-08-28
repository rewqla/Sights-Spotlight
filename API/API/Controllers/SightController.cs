using API.Contract.Requests.Sight;
using API.Contract.Responses.Country;
using API.Contract.Responses.Sight;
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
        [ResponseCache(Duration = 60, VaryByQueryKeys = new[] { "country", "yearOfFoundationFrom", "YearOfFoundationTo", "page", "pageSize" }, VaryByHeader = "Accept, Accept-Encoding", Location = ResponseCacheLocation.Any)]
        public async Task<ActionResult<SightsResponse>> GetSights([FromQuery] GetAllSightsRequest request, CancellationToken cancellationToken)
        {
            var sights = await _sightsService.GetAllSights(request, cancellationToken);

            return Ok(sights);
        }
    }
}
