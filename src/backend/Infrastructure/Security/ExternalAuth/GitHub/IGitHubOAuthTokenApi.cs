using Refit;

namespace Infrastructure.Security.ExternalAuth.GitHub;

public interface IGitHubOAuthTokenApi
{
    [Post("/login/oauth/access_token")]
    [Headers("Accept: application/json")]
    Task<ApiResponse<GitHubTokenResponse>> ExchangeCodeAsync([Body(BodySerializationMethod.UrlEncoded)] Dictionary<string, string> data);
}