using API.Contract.Requests;
using API.Contract.Responses;
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
        public AccountController(UserManager<User> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        [HttpPost(AccountRoutes.Register)]
        public async Task<ActionResult<UserResponse>> RegisterUser(RegisterRequest registerRequest)
        {
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
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                return ValidationProblem();
            }

            await _userManager.AddToRoleAsync(user, "Member");

            var createdUser = await _userManager.FindByNameAsync(registerRequest.Username);

            return new UserResponse
            {
                Email = user.Email,
                Token = await _tokenService.GenerateToken(createdUser!),
            };
        }

        [HttpPost(AccountRoutes.Login)]
        public async Task<ActionResult<UserResponse>> Login(LoginRequest loginRequest)
        {
            var user = await _userManager.FindByNameAsync(loginRequest.Username);

            if (user == null || !await _userManager.CheckPasswordAsync(user, loginRequest.Password))
                return Unauthorized();

            return new UserResponse
            {
                Email = user.Email,
                Token = await _tokenService.GenerateToken(user),
            };
        }

        [Authorize]
        [HttpGet(AccountRoutes.CurrentUser)]
        public async Task<ActionResult<UserResponse>> GetCurrentUser()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            return new UserResponse
            {
                Email = user.Email,
                Token = token,
            };
        }
    }
}
