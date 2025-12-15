using Web.Api;
using Web.Communication.Requests.Auth;
using Web.Communication.Responses.Auth;

namespace Web.Services.Auth;

public class AuthService : IAuthService
{
    private readonly IAuthApi _authApi;
    private string? _jwtToken;
    private Dictionary<string, string>? _userClaims;

    public Dictionary<string, string>? UserClaims => _userClaims;

    public AuthService(IAuthApi authApi)
    {
        _authApi = authApi;
    }

    // Login social
    public async Task<AuthLoginResponse?> LoginAsync(string provider, string code)
    {
        var request = new AuthLoginRequest(provider, code);
        var response = await _authApi.Login(request);

        if (response.IsSuccessful && !string.IsNullOrEmpty(response.Content.Token))
        {
            _jwtToken = response.Content.Token;


        }

        return response.Content;
    }

    // Redirecionamento para provider (backend retorna a URL)
    public async Task<AuthRedirectToProviderResponse?> GetProviderRedirectUrlAsync(string provider)
    {
        var response = await _authApi.RedirectToProvider(provider);

        return response.Content;
    }

    // Token
    public string? GetToken() => _jwtToken;

    // Pegar dados do usuário a partir do token JWT
    public Dictionary<string, string>? GetUserClaims(string token)
    {
        // Decodificar JWT para pegar claims
        var handler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
        var jwt = handler.ReadJwtToken(token);

        _userClaims = jwt.Claims.ToDictionary(c => c.Type, c => c.Value);
        return UserClaims;
    }

    // Logout
    public void Logout() => _jwtToken = null;

    // Verifica login
    public bool IsLoggedIn() => !string.IsNullOrEmpty(_jwtToken);
}