using API.Contract.Requests.Sight;
using API.Routes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using StoreBLL.Interfaces;

namespace API.Endpoints.Sights;
public static class GetSightsEndpoint
{
    public const string Name = "GetSights";
    public static IEndpointRouteBuilder MapGetSights(this IEndpointRouteBuilder app)
    {
        app.MapGet(SightEndpoints.GetAll, async ([AsParameters] GetAllSightsRequest request, ILogger<Program> logger,
            IOutputCacheStore _outputCacheStore, ISightService _sightsService, CancellationToken cancellationToken) =>
        {     
            logger.LogInformation("GetSights called with Country: {Country}, YearOfFoundationFrom: {YearOfFoundationFrom}, YearOfFoundationTo: {YearOfFoundationTo}, Page: {Page}, PageSize: {PageSize}",
            request.Country, request.YearOfFoundationFrom, request.YearOfFoundationTo, request.Page, request.PageSize);

            var sights = await _sightsService.GetAllSights(request, cancellationToken);

            //also add at update, remove, add operations
            logger.LogInformation("Evicting cache for 'sights'");
            await _outputCacheStore.EvictByTagAsync("sights", cancellationToken);

            return TypedResults.Ok(sights);
        })
            .WithName(Name)
            .CacheOutput("SightCache");

        return app;
    }
}
