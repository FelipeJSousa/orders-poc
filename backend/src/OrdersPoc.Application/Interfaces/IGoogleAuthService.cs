using OrdersPoc.Application.DTOs;

namespace OrdersPoc.Application.Interfaces;

public interface IGoogleAuthService
{
    Task<GoogleTokenResponse> ExchangeCodeForTokensAsync(string code);
    Task<GoogleUserInfo> GetUserInfoAsync(string accessToken);
}