using API.Endpoints.Account;
using API.Endpoints.Country;
using API.Endpoints.Sights;

namespace API.Endpoints;
public static class EndpointsExtensions
{
    public static IEndpointRouteBuilder MapApiEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapSightsEndpoints();
        app.MapCountryEndpoints();
        app.MapAccountEndpoints();

        return app;
    }
}

