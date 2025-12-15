using Web.Communication.Responses.Auth;

namespace Web.Services.Auth;

public interface IAuthService
{
    Task<AuthLoginResponse> LoginAsync(string provider, string code);
    Task<AuthRedirectToProviderResponse> GetProviderRedirectUrlAsync(string provider);
    string? GetToken();
    void Logout();
    bool IsLoggedIn();
    Dictionary<string, string>? GetUserClaims(string token);
}