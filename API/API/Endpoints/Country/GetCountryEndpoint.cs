using API.Authorization;
using API.Contract.Requests.Sight;
using API.Contract.Responses.Country;
using API.Routes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.Extensions.Logging;
using StoreBLL.Interfaces;
using StoreBLL.Services;

namespace API.Endpoints.Country;
public static class GetCountryEndpoint
{
    public const string Name = "GetCountry";
    public static IEndpointRouteBuilder MapGetCountry(this IEndpointRouteBuilder app)
    {
        app.MapGet(CountryEndpoints.GetById, async (int id,
             ILogger<Program> logger,
           [FromServices] ICountryService countryService,
            CancellationToken cancellationToken) =>
        {
            logger.LogInformation("Fetching details for country with ID {CountryId}.", id);
            var country = await countryService.GetCountryDetailsById(id, cancellationToken);

            if (country == null)
            {
                logger.LogWarning("Country with ID {CountryId} not found.", id);
                return Results.NotFound();
            }

            logger.LogInformation("Country with ID {CountryId} fetched successfully.", id);

            return Results.Ok(country);
        })
            .WithName(Name)
            .RequireAuthorization(PolicyRoles.Admin);


        return app;
    }
}
