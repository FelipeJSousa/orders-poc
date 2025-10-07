namespace OrdersPoc.Application.DTOs;

public class GoogleAuthCodeRequest
{
    public string Code { get; set; } = string.Empty;
}

public class GoogleTokenResponse
{
    public string Access_token { get; set; } = string.Empty;
    public string Id_token { get; set; } = string.Empty;
    public string Refresh_token { get; set; } = string.Empty;
    public int Expires_in { get; set; }
    public string Token_type { get; set; } = string.Empty;
    public string Scope { get; set; } = string.Empty;
}

public class GoogleUserInfo
{
    public string Sub { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Given_name { get; set; } = string.Empty;
    public string Family_name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool Email_verified { get; set; }
    public string Picture { get; set; } = string.Empty;
}