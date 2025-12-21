using Communication.Requests.Auth;
using Communication.Responses.Auth;

namespace Application.UseCases.Auth.Login;

public interface IAuthLoginUseCase
{
    Task<AuthLoginResponse> Execute(AuthLoginRequest request);
}
