using System.Text.Json;
using Microsoft.Extensions.Configuration;
using OrdersPoc.Application.DTOs;
using OrdersPoc.Application.Interfaces;

namespace OrdersPoc.Application.Services;

public class GoogleAuthService : IGoogleAuthService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public GoogleAuthService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<GoogleTokenResponse> ExchangeCodeForTokensAsync(string code)
    {
        var clientId = _configuration["GoogleOAuth:ClientId"];
        var clientSecret = _configuration["GoogleOAuth:ClientSecret"];
        var redirectUri = _configuration["GoogleOAuth:RedirectUri"];

        var tokenRequest = new Dictionary<string, string>
        {
            { "code", code },
            { "client_id", clientId! },
            { "client_secret", clientSecret! },
            { "redirect_uri", redirectUri! },
            { "grant_type", "authorization_code" }
        };

        var response = await _httpClient.PostAsync(
            "https://oauth2.googleapis.com/token",
            new FormUrlEncodedContent(tokenRequest)
        );

        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var tokenResponse = JsonSerializer.Deserialize<GoogleTokenResponse>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return tokenResponse ?? throw new InvalidOperationException("Failed to exchange code for tokens");
    }

    public async Task<GoogleUserInfo> GetUserInfoAsync(string accessToken)
    {
        _httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

        var response = await _httpClient.GetAsync("https://www.googleapis.com/oauth2/v2/userinfo");

        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var userInfo = JsonSerializer.Deserialize<GoogleUserInfo>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return userInfo ?? throw new InvalidOperationException("Failed to get user info");
    }
}
