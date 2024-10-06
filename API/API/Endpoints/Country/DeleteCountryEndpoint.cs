using API.Authorization;
using API.Contract.Requests.Sight;
using API.Contract.Responses.Country;
using API.Routes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using StoreBLL.Interfaces;
using StoreBLL.Services;

namespace API.Endpoints.Country;
public static class DeleteCountryEndpoint
{
    private const string Name = "DeleteCountry";
    public static IEndpointRouteBuilder MapDeleteCountry(this IEndpointRouteBuilder app)
    {
        app.MapDelete(CountryEndpoints.Delete, async (int id, ILogger<Program> logger,
            ICountryService countryService, CancellationToken cancellationToken) =>
        {
            logger.LogInformation("Deleting country with ID {CountryId}.", id);

            var result = await countryService.DeleteCountry(id, cancellationToken);

            if (!result)
            {
                logger.LogWarning("Country with ID {CountryId} not found for deletion.", id);
                return Results.NotFound(new { message = "Country not found" });
            }

            logger.LogInformation("Country with ID {CountryId} deleted successfully.", id);
            return Results.NoContent();
        })
            .WithName(Name)
            .WithTags("Country")
            .RequireAuthorization(PolicyRoles.Admin); 

        return app;
    }
}
