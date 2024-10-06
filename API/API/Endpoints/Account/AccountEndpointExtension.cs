namespace API.Endpoints.Account;
public static class AccountEndpointExtension
{
    public static IEndpointRouteBuilder MapAccountEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapLogin();
        app.MapRegister();
        app.MapCurrentUser();

        return app;
    }
}
