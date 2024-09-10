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
public static class LoginEndpoint
{
    public const string Name = "Login";
    public static IEndpointRouteBuilder MapLogin(this IEndpointRouteBuilder app)
    {
        app.MapPost(AccountEndpoints.Login, async (LoginRequest loginRequest, ILogger<Program> logger, UserManager<User> _userManager, ITokenService _tokenService) =>
        {
            logger.LogInformation("User attempting to log in: {Username}", loginRequest.Username);

            var user = await _userManager.FindByNameAsync(loginRequest.Username);

            if (user == null || !await _userManager.CheckPasswordAsync(user, loginRequest.Password))
            {
                logger.LogWarning($"Login failed for {loginRequest.Username}: Invalid username or password");
                return Results.Unauthorized();
            }

            logger.LogInformation($"User {loginRequest.Username} successfully logged in");

            var response = new UserResponse
            {
                Email = user.Email,
                Token = await _tokenService.GenerateToken(user),
            };

            return TypedResults.Ok(response);
        })
            .WithName(Name);

        return app;
    }
}
