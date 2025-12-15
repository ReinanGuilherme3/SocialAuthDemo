using Domain.Security.ExternalAuth;
using Microsoft.Extensions.Options;
using System.Web;

namespace Infrastructure.Security.ExternalAuth.GitHub;

public class GitHubAuthProvider : IExternalAuthProvider
{
    public string Name => "github";

    private readonly GitHubOAuthSettings _settings;
    private readonly IGitHubOAuthTokenApi _tokenApi;
    private readonly IGitHubUserApi _userApi;

    public GitHubAuthProvider(
        IOptions<GitHubOAuthSettings> settings,
        IGitHubOAuthTokenApi tokenApi,
        IGitHubUserApi userApi)
    {
        _settings = settings.Value;
        _tokenApi = tokenApi;
        _userApi = userApi;
    }

    public string GenerateLoginUrl()
    {
        var query = HttpUtility.ParseQueryString(string.Empty);

        query["client_id"] = _settings.ClientId;
        query["redirect_uri"] = _settings.RedirectUri;
        query["scope"] = "read:user user:email";

        return $"https://github.com/login/oauth/authorize?{query}";
    }

    public async Task<ExternalUserData> AuthenticateAsync(string code)
    {
        var data = new Dictionary<string, string>
        {
            ["client_id"] = _settings.ClientId,
            ["client_secret"] = _settings.ClientSecret,
            ["code"] = code,
            ["redirect_uri"] = _settings.RedirectUri
        };

        var tokenResponse = await _tokenApi.ExchangeCodeAsync(data);

        if (!tokenResponse.IsSuccessStatusCode || tokenResponse.Content is null)
            throw new InvalidOperationException(
                $"GitHub token error: {tokenResponse.Error?.Content}");

        var accessToken = tokenResponse.Content.access_token;

        var userResponse = await _userApi.GetUserAsync($"Bearer {accessToken}");

        if (!userResponse.IsSuccessStatusCode || userResponse.Content is null)
            throw new InvalidOperationException(
                $"GitHub user error: {userResponse.Error?.Content}");

        var user = userResponse.Content;

        return new ExternalUserData(
            ExternalId: user.id.ToString(),
            Email: user.email ?? string.Empty,
            Name: user.name ?? user.login,
            Picture: user.avatar_url
        );
    }

}
