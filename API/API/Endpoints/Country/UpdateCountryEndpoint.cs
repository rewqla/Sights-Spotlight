using API.Authorization;
using API.Contract.Requests;
using API.Contract.Requests.Sight;
using API.Contract.Responses.Country;
using API.Controllers;
using API.Routes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using StoreBLL.Interfaces;
using StoreBLL.Services;
using StoreDAL.Data.Migrations;

namespace API.Endpoints.Country;
public static class UpdateCountryEndpoint
{
    public const string Name = "UpdateCountry";
    public static IEndpointRouteBuilder MapUpdateCountry(this IEndpointRouteBuilder app)
    {
        app.MapPut(CountryRoutes.Update, async (UpdateCountryRequest updateCountry, ILogger < Program > logger,
            ICountryService _countryService, CancellationToken cancellationToken) =>
        {
            logger.LogInformation("Updating country with ID {CountryId}.", updateCountry.Id);

            var result = await _countryService.UpdateCountry(updateCountry, cancellationToken);

            if (!result)
            {
                logger.LogWarning("Country with ID {CountryId} not found for update.", updateCountry.Id);
                return Results.NotFound();
            }

            logger.LogInformation("Country with ID {CountryId} updated successfully.", updateCountry.Id);
            return Results.NoContent();
        })
            .WithName(Name)
            .RequireAuthorization(PolicyRoles.Admin);

        return app;
    }
}
