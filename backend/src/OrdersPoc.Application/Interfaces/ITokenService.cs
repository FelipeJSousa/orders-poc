namespace OrdersPoc.Application.Interfaces;

public interface ITokenService
{
    string GenerateToken(string userId, string userName, string email, List<string>? roles = null);
    bool ValidateToken(string token);
}