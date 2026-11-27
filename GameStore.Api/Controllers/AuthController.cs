using GameStore.Api.Configuration;
using GameStore.Api.Data;
using GameStore.Api.Features.Authentication.DTOs;
using GameStore.Api.Features.Authentication.Services;
using GameStore.Api.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly GameStoreDbContext _dbContext;
        private readonly ITokenService _tokenService;
        private readonly JwtOptions _jwtOptions;

        public AuthController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            GameStoreDbContext dbContext,
            ITokenService tokenService,
            IOptions<JwtOptions> jwtOptions)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dbContext = dbContext;
            _tokenService = tokenService;
            _jwtOptions = jwtOptions.Value;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(
            RegisterRequest request)
        {
            var user = new ApplicationUser
            {
                DisplayName = request.DisplayName.Trim(),
                Email = request.Email.Trim(),
                UserName = request.Email.Trim(),
            };


            var result = await _userManager.CreateAsync(
                user,
                request.Password);

            if (!result.Succeeded)
            {
                return BadRequest(new
                {
                    errors = result.Errors.Select(x => new
                    {
                        x.Code,
                        x.Description
                    })
                });
            }

            var roleResult = await _userManager.AddToRoleAsync(
                user,
                "User");

            if (!roleResult.Succeeded)
            {
                await _userManager.DeleteAsync(user);
                return StatusCode(500, new
                {
                    message = "Unable to complete account registration."
                });
            }

            var authResponse = await CreateAuthResponseAsync(user);
            return StatusCode(200, authResponse);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(
        LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(
                request.Email.Trim());

            if (user is null)
                return Unauthorized(new { message = "Invalid credentials." });

            var result = await _signInManager.CheckPasswordSignInAsync(
                user,
                request.Password,
                lockoutOnFailure: true);

            if (!result.Succeeded)
            {
                return Unauthorized(new
                {
                    message = "Invalid credentials."
                });
            }

            await _userManager.ResetAccessFailedCountAsync(user);

            var authResponse = await CreateAuthResponseAsync(user);

            return Ok(authResponse);
        }

        private async Task<AuthResponse> CreateAuthResponseAsync(
            ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            var accessToken = _tokenService.GenerateAccessToken(
                user,
                roles);

            var refreshToken = _tokenService.GenerateRefreshToken();

            var now = DateTime.UtcNow;

            var refreshTokenEntity = new RefreshToken
            {
                UserId = user.Id,
                TokenHash = refreshToken.Hash,
                FamilyId = Guid.NewGuid(),
                CreatedByIp = HttpContext.Connection.RemoteIpAddress?.ToString(),
                ExpiresAtUtc = now.AddDays(
                    _jwtOptions.RefreshTokenExpirationDays)
            };

            _dbContext.RefreshTokens.Add(refreshTokenEntity);

            await _dbContext.SaveChangesAsync();

            return new AuthResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token,
                AccessTokenExpiresAtUtc = now.AddMinutes(
                    _jwtOptions.AccessTokenExpirationMinutes),
                RefreshTokenExpiresAtUtc = refreshTokenEntity.ExpiresAtUtc,
                UserId = user.Id,
                Email = user.Email!,
                DisplayName = user.DisplayName,
                Roles = roles
            };
        }
    }
}
