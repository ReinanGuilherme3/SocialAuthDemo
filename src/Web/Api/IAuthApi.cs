using Refit;
using Web.Communication.Requests.Auth;
using Web.Communication.Responses.Auth;

namespace Web.Api;

public interface IAuthApi
{
    [Post("/auth/login")]
    Task<ApiResponse<AuthLoginResponse>> Login([Body] AuthLoginRequest request);

    [Get("/auth/redirect-to-provider")]
    Task<ApiResponse<AuthRedirectToProviderResponse>> RedirectToProvider([Query] string provider);
}
