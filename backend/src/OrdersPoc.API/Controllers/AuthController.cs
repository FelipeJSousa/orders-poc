using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrdersPoc.Application.DTOs;
using OrdersPoc.Application.Interfaces;

namespace OrdersPoc.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class AuthController : ControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly IGoogleAuthService _googleAuthService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(
        ITokenService tokenService,
        IGoogleAuthService googleAuthService,
        ILogger<AuthController> logger)
    {
        _tokenService = tokenService;
        _googleAuthService = googleAuthService;
        _logger = logger;
    }

    /// <summary>
    /// Troca authorization code do Google por JWT próprio
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GoogleCallback([FromBody] GoogleAuthCodeRequest request)
    {
        try
        {
            _logger.LogInformation("Processing Google callback with code");

            var tokenResponse = await _googleAuthService.ExchangeCodeForTokensAsync(request.Code);

            var userInfo = await _googleAuthService.GetUserInfoAsync(tokenResponse.Access_token);

            _logger.LogInformation("User authenticated: {Email}", userInfo.Email);

            var jwtToken = _tokenService.GenerateToken(
                userId: userInfo.Sub,
                userName: userInfo.Name,
                email: userInfo.Email,
                roles: new List<string> { "User" }
            );

            return Ok(new
            {
                token = jwtToken,
                user = new
                {
                    id = userInfo.Sub,
                    name = userInfo.Name,
                    email = userInfo.Email,
                    picture = userInfo.Picture
                },
                expiresIn = 3600
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during Google authentication");
            return BadRequest(new { message = "Erro ao autenticar com Google", error = ex.Message });
        }
    }
}