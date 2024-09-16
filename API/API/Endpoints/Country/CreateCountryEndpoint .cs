using API.Authorization;
using API.Contract.Requests.Country;
using API.Routes;
using Microsoft.AspNetCore.Mvc;
using StoreBLL.Interfaces;

namespace API.Endpoints.Country;
public static class CreateCountryEndpoint
{
    private const string Name = "CreateCountry";
    public static IEndpointRouteBuilder MapCreateCountry(this IEndpointRouteBuilder app)
    {
        app.MapPost(CountryEndpoints.Create, async (CreateCountryRequest createCountry, ILogger<Program> logger,
            ICountryService countryService, CancellationToken cancellationToken) =>
        {
            logger.LogInformation("Creating a new country with name {CountryName}.", createCountry.Name);

            int createdCountryId = await countryService.CreateCountry(createCountry, cancellationToken);
            logger.LogInformation("Country created successfully with ID {CountryId}.", createdCountryId);

            return TypedResults.CreatedAtRoute(createdCountryId, GetCountryEndpoint.Name, new { id = createdCountryId });
        })
            .WithName(Name)
             .RequireAuthorization(PolicyRoles.Member);

        return app;
    }
}
