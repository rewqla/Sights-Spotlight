using API.Authorization;
using API.Contract.Requests.Country;
using API.Routes;
using StoreBLL.Interfaces;

namespace API.Endpoints.Country;
public static class CreateCountryEndpoint
{
    public const string Name = "CreateCountry";
    public static IEndpointRouteBuilder MapCreateCountry(this IEndpointRouteBuilder app)
    {
        app.MapPost(CountryRoutes.Create, async (CreateCountryRequest createCountry, ILoggerFactory loggerFactory,
            ICountryService _countryService, CancellationToken cancellationToken) =>
        {
            var logger = loggerFactory.CreateLogger("CreateCountryEndpoint");
            logger.LogInformation("Creating a new country with name {CountryName}.", createCountry.Name);

            int createdCountryId = await _countryService.CreateCountry(createCountry, cancellationToken);
            logger.LogInformation("Country created successfully with ID {CountryId}.", createdCountryId);

            return TypedResults.CreatedAtRoute(createdCountryId, GetCountryEndpoint.Name, new { id = createdCountryId });
        })
            .WithName(Name)
             .RequireAuthorization(PolicyRoles.Member);

        return app;
    }
}
