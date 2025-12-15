using Google.Apis.Auth;
using Microsoft.Extensions.Options;
using System.Web;

namespace Infrastructure.Security.ExternalAuth.Google;

public class GoogleAuthProvider : IExternalAuthProvider
{
    public string Name => "google";

    private readonly GoogleOAuthSettings _settings;
    private readonly IGoogleOAuthApi _googleApi;

    public GoogleAuthProvider(
        IOptions<GoogleOAuthSettings> settings,
        IGoogleOAuthApi googleApi)
    {
        _settings = settings.Value;
        _googleApi = googleApi;
    }

    public string GenerateLoginUrl()
    {
        var query = HttpUtility.ParseQueryString(string.Empty);

        query["client_id"] = _settings.ClientId;
        query["redirect_uri"] = _settings.RedirectUri;
        query["response_type"] = "code";
        query["scope"] = "openid email profile";
        query["access_type"] = "offline";

        return $"https://accounts.google.com/o/oauth2/v2/auth?{query}";
    }

    public async Task<ExternalUserData> AuthenticateAsync(string code)
    {
        var data = new Dictionary<string, string>
        {
            ["code"] = code,
            ["client_id"] = _settings.ClientId,
            ["client_secret"] = _settings.ClientSecret,
            ["redirect_uri"] = _settings.RedirectUri,
            ["grant_type"] = "authorization_code"
        };

        var token = await _googleApi.ExchangeCodeAsync(data);

        if (string.IsNullOrEmpty(token.Content.id_token))
            throw new InvalidOperationException("Invalid Google id_token");

        var payload = await GoogleJsonWebSignature.ValidateAsync(
            token.Content.id_token,
            new GoogleJsonWebSignature.ValidationSettings
            {
                Audience = new[]
                {
                    _settings.ClientId
                }
            });

        return new ExternalUserData(
            ExternalId: payload.Subject,
            Email: payload.Email,
            Name: payload.Name,
            Picture: payload.Picture
        );
    }
}
