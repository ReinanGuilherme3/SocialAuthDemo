using Refit;

namespace Infrastructure.Security.ExternalAuth.Google;

public interface IGoogleOAuthApi
{
    [Post("/token")]
    Task<ApiResponse<GoogleTokenResponse>> ExchangeCodeAsync([Body(BodySerializationMethod.UrlEncoded)] Dictionary<string, string> data);
}