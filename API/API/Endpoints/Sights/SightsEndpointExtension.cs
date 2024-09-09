namespace API.Endpoints.Sights;
public static class SightsEndpointExtension
{
    public static IEndpointRouteBuilder MapSightsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGetSights();

        return app;
    }
}
