using API.Contract.Requests.Account;
using API.Contract.Responses.Account;
using API.Routes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreBLL.Interfaces;
using StoreBLL.Services;
using StoreDAL.Entities;
using System.IdentityModel.Tokens.Jwt;

namespace API.Controllers
{
    [ApiController]

    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        private readonly ILogger<AccountController> _logger;
        public AccountController(UserManager<User> userManager, ITokenService tokenService, ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _logger = logger;
        }

        [HttpPost(AccountRoutes.Register)]
        public async Task<ActionResult<UserResponse>> RegisterUser(RegisterRequest registerRequest)
        {
            _logger.LogInformation("Registering a new user: {Username}", registerRequest.Username);

            var user = new User
            {
                UserName = registerRequest.Username,
                Email = registerRequest.Email,
                FirstName = registerRequest.FirstName,
                LastName = registerRequest.LastName,
            };

            var result = await _userManager.CreateAsync(user, registerRequest.Password);

            if (!result.Succeeded)
            {
                _logger.LogWarning($"User registration failed for {registerRequest.Username}: {string.Join(", ", result.Errors.Select(e => e.Description))}");

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                return ValidationProblem();
            }

            await _userManager.AddToRoleAsync(user, "Member");

            var createdUser = await _userManager.FindByNameAsync(registerRequest.Username);

            _logger.LogInformation($"User {registerRequest.Username} successfully registered");

            return new UserResponse
            {
                Email = user.Email,
                Token = await _tokenService.GenerateToken(createdUser!),
            };
        }

        //[HttpPost(AccountRoutes.Login)]
        //public async Task<ActionResult<UserResponse>> Login(LoginRequest loginRequest)
        //{
        //    _logger.LogInformation("User attempting to log in: {Username}", loginRequest.Username);

        //    var user = await _userManager.FindByNameAsync(loginRequest.Username);

        //    if (user == null || !await _userManager.CheckPasswordAsync(user, loginRequest.Password))
        //    {
        //        _logger.LogWarning($"Login failed for {loginRequest.Username}: Invalid username or password");
        //        return Unauthorized();
        //    }

        //    _logger.LogInformation($"User {loginRequest.Username} successfully logged in");

        //    return new UserResponse
        //    {
        //        Email = user.Email,
        //        Token = await _tokenService.GenerateToken(user),
        //    };
        //}

        [Authorize]
        [HttpGet(AccountRoutes.CurrentUser)]
        public async Task<ActionResult<UserResponse>> GetCurrentUser()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            _logger.LogInformation($"Retriving current user data for {user.UserName}");

            return new UserResponse
            {
                Email = user.Email,
                Token = token,
            };
        }
    }
}
