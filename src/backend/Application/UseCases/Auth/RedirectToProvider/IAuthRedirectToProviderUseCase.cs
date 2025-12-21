using Communication.Requests.Auth;
using Communication.Responses.Auth;

namespace Application.UseCases.Auth.RedirectToProvider;

public interface IAuthRedirectToProviderUseCase
{
    AuthRedirectToProviderResponse Execute(AuthRedirectToProviderRequest request);
}