using API.Authorization;
using API.Contract.Requests.Sight;
using API.Contract.Responses.Country;
using API.Controllers;
using API.Routes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using StoreBLL.Interfaces;
using StoreBLL.Services;

namespace API.Endpoints.Country;
public static class DeleteCountryEndpoint
{
    public const string Name = "DeleteCountry";
    public static IEndpointRouteBuilder MapDeleteCountry(this IEndpointRouteBuilder app)
    {
        app.MapDelete(CountryRoutes.Delete, async (int id, ILoggerFactory loggerFactory,
            ICountryService _countryService, CancellationToken cancellationToken) =>
        {
            var logger = loggerFactory.CreateLogger("DeleteCountryEndpoint");

            logger.LogInformation("Deleting country with ID {CountryId}.", id);

            var result = await _countryService.DeleteCountry(id, cancellationToken);

            if (!result)
            {
                logger.LogWarning("Country with ID {CountryId} not found for deletion.", id);
                return Results.NotFound(new { message = "Country not found" });
            }

            logger.LogInformation("Country with ID {CountryId} deleted successfully.", id);
            return Results.NoContent();
        })
            .WithName(Name)
            .RequireAuthorization(PolicyRoles.Admin); 

        return app;
    }
}
