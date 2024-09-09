using API.Endpoints.Sights;

namespace API.Endpoints.Country;
public static class CountryEndpointExtension
{
    public static IEndpointRouteBuilder MapCountryEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGetCountries();
        app.MapGetCountry();
        app.MapDeleteCountry();
        app.MapCreateCountry();
        app.MapUpdateCountry();

        return app;
    }
}

