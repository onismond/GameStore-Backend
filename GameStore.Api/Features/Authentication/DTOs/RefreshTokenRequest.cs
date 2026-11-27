using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Features.Authentication.DTOs;

public class RefreshTokenRequest
{
    [Required]
    public string RefreshToken { get; set; } = string.Empty;
}