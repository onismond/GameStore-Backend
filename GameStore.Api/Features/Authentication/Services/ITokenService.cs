using GameStore.Api.Models.Entities;

namespace GameStore.Api.Features.Authentication.Services;

public interface ITokenService
{
    string GenerateAccessToken(
        ApplicationUser user,
        IList<string> roles);

    (string Token, string Hash) GenerateRefreshToken();

    string HashRefreshToken(string token);
}