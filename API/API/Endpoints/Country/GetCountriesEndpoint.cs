using API.Contract.Requests.Sight;
using API.Contract.Responses.Country;
using API.Routes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using StoreBLL.Interfaces;
using StoreBLL.Services;

namespace API.Endpoints.Country;
public static class GetCountriesEndpoint
{
    private const string Name = "GetCountries";
    public static IEndpointRouteBuilder MapGetCountries(this IEndpointRouteBuilder app)
    {
        app.MapGet(CountryEndpoints.GetAll, async (ILogger<Program> logger,
            ICountryService countryService, CancellationToken cancellationToken) =>
        {
            logger.LogInformation("Fetching all countries.");

            var countries = await countryService.GetAllCountries(cancellationToken);
            logger.LogInformation("Fetched {Count} countries.", countries.Count());

            return TypedResults.Ok(countries);
        })
            .WithName(Name)
            .WithTags("Country");


        return app;
    }
}
