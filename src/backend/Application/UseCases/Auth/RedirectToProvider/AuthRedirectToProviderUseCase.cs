using Communication.Requests.Auth;
using Communication.Responses.Auth;
using Domain.Security.ExternalAuth;

namespace Application.UseCases.Auth.RedirectToProvider;


internal sealed class AuthRedirectToProviderUseCase : IAuthRedirectToProviderUseCase
{
    private readonly IExternalAuthProviderResolver _externalAuthProviderResolver;

    public AuthRedirectToProviderUseCase(IExternalAuthProviderResolver externalAuthProviderResolver)
    {
        _externalAuthProviderResolver = externalAuthProviderResolver;
    }

    public AuthRedirectToProviderResponse Execute(AuthRedirectToProviderRequest request)
    {

        var authProvider = _externalAuthProviderResolver.Resolve(request.Provider);

        var url = authProvider.GenerateLoginUrl();

        return new AuthRedirectToProviderResponse(url);
    }
}
