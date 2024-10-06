using API.Contract.Requests.Account;
using API.Contract.Requests.Sight;
using API.Contract.Responses.Account;
using API.Endpoints.Country;
using API.Routes;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using StoreBLL.Interfaces;
using StoreBLL.Services;
using StoreDAL.Entities;

namespace API.Endpoints.Account;
public static class CurrentUserEndpoint
{
    private const string Name = "CurrentUser";
    public static IEndpointRouteBuilder MapCurrentUser(this IEndpointRouteBuilder app)
    {
        app.MapPost(AccountEndpoints.CurrentUser, async ( ILogger<Program> logger, UserManager<User> userManager, ITokenService tokenService, HttpContext httpContext) =>
        {
            var user = await userManager.FindByNameAsync(httpContext.User.Identity.Name);

            var token = httpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            logger.LogInformation($"Retrieving current user data for {user.UserName}");

            var response= new UserResponse
            {
                Email = user.Email!,
                Token = token,
            };

            return TypedResults.Ok(response);
        })
            .WithName(Name)
            .WithTags("Account");

        return app;
    }
}
