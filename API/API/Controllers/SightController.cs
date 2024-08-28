using API.Contract.Requests.Sight;
using API.Contract.Responses.Country;
using API.Contract.Responses.Sight;
using API.Routes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using StoreBLL.Interfaces;

namespace API.Controllers
{
    [ApiController]
    public class SightController : ControllerBase
    {
        private readonly ISightService _sightsService;
        private readonly IOutputCacheStore _outputCacheStore;
        public SightController(ISightService sightsService, IOutputCacheStore outputCacheStore)
        {
            _sightsService = sightsService;
            _outputCacheStore = outputCacheStore;
        }

        [HttpGet(SightRoutes.GetAll)]
        [OutputCache(PolicyName = "SightCache")]
        //[ResponseCache(Duration = 60, VaryByQueryKeys = new[] { "country", "yearOfFoundationFrom", "YearOfFoundationTo", "page", "pageSize" }, VaryByHeader = "Accept, Accept-Encoding", Location = ResponseCacheLocation.Any)]
        public async Task<ActionResult<SightsResponse>> GetSights([FromQuery] GetAllSightsRequest request, CancellationToken cancellationToken)
        {
            var sights = await _sightsService.GetAllSights(request, cancellationToken);

            //also add at update, remove, add operations
            await _outputCacheStore.EvictByTagAsync("sights", cancellationToken);

            return Ok(sights);
        }
    }
}
