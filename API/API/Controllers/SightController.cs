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
        private readonly ILogger<SightController> _logger;

        public SightController(ISightService sightsService, IOutputCacheStore outputCacheStore, ILogger<SightController> logger)
        {
            _sightsService = sightsService;
            _outputCacheStore = outputCacheStore;
            _logger = logger;
        }

        //[HttpGet(SightRoutes.GetAll)]
        //[OutputCache(PolicyName = "SightCache")]
        ////[ResponseCache(Duration = 60, VaryByQueryKeys = new[] { "country", "yearOfFoundationFrom", "YearOfFoundationTo", "page", "pageSize" }, VaryByHeader = "Accept, Accept-Encoding", Location = ResponseCacheLocation.Any)]
        //public async Task<ActionResult<SightsResponse>> GetSights([FromQuery] GetAllSightsRequest request, CancellationToken cancellationToken)
        //{
        //    _logger.LogInformation("GetSights called with Country: {Country}, YearOfFoundationFrom: {YearOfFoundationFrom}, YearOfFoundationTo: {YearOfFoundationTo}, Page: {Page}, PageSize: {PageSize}",
        //       request.Country, request.YearOfFoundationFrom, request.YearOfFoundationTo, request.Page, request.PageSize);

        //    var sights = await _sightsService.GetAllSights(request, cancellationToken);

        //    //also add at update, remove, add operations
        //    _logger.LogInformation("Evicting cache for 'sights'");
        //    await _outputCacheStore.EvictByTagAsync("sights", cancellationToken);

        //    return Ok(sights);
        //}
    }
}
