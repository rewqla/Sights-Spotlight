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
public static class RegisterEndpoint
{
    private const string Name = "Register";
    public static IEndpointRouteBuilder MapRegister(this IEndpointRouteBuilder app)
    {
        app.MapPost(AccountEndpoints.Register, async (RegisterRequest registerRequest, ILogger<Program> logger, UserManager<User> userManager, ITokenService tokenService) =>
        {
            logger.LogInformation("Registering a new user: {Username}", registerRequest.Username);

            var user = new User
            {
                UserName = registerRequest.Username,
                Email = registerRequest.Email,
                FirstName = registerRequest.FirstName,
                LastName = registerRequest.LastName,
            };

            var result = await userManager.CreateAsync(user, registerRequest.Password);

            if (!result.Succeeded)
            {
                logger.LogWarning($"User registration failed for {registerRequest.Username}: {string.Join(", ", result.Errors.Select(e => e.Description))}");

                var validationErrors = new Dictionary<string, string[]>();

                foreach (var error in result.Errors)
                {
                    validationErrors.Add(error.Code, new[] { error.Description });
                }

                return Results.ValidationProblem(validationErrors);
            }

            await userManager.AddToRoleAsync(user, "Member");

            var createdUser = await userManager.FindByNameAsync(registerRequest.Username);

            logger.LogInformation($"User {registerRequest.Username} successfully registered");

            var response = new UserResponse
            {
                Email = user.Email,
                Token = await tokenService.GenerateToken(createdUser!),
            };

            return TypedResults.Ok(response);
        })
            .WithName(Name)
            .WithTags("Account");;

        return app;
    }
}
