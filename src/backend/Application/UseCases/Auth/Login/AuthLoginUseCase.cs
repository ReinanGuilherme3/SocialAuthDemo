using Application.Contracts.Auth;
using Communication.Requests.Auth;
using Communication.Responses.Auth;
using Domain.Entities;
using Domain.Repositories;
using Domain.Security.ExternalAuth;

namespace Application.UseCases.Auth.Login;

internal sealed class AuthLoginUseCase : IAuthLoginUseCase
{
    private readonly IExternalAuthProviderResolver _externalAuthProviderResolver;
    private readonly IUserRepository _userRepository;
    private readonly IToken _jwtService;

    public AuthLoginUseCase(
        IExternalAuthProviderResolver externalAuthProviderResolver,
        IUserRepository userRepository,
        IToken jwtService)
    {
        _externalAuthProviderResolver = externalAuthProviderResolver;
        _userRepository = userRepository;
        _jwtService = jwtService;
    }

    public async Task<AuthLoginResponse> Execute(AuthLoginRequest request)
    {
        // Resolve provider (Google, Apple, etc.)
        var provider = _externalAuthProviderResolver.Resolve(request.Provider);

        // Autentica usuário no provider externo
        var externalUser = await provider.AuthenticateAsync(request.Code);

        if (externalUser is null)
            return new AuthLoginResponse("", "", false, "Authentication failed");

        // Verifica se usuário já existe no sistema
        var user = await _userRepository.GetByExternalProviderAsync(
            request.Provider,
            externalUser.ExternalId);

        if (user is null)
            user = await CreateUserAsync(request.Provider, externalUser);

        // Gera token JWT
        var token = _jwtService.GenerateToken(user);

        return new AuthLoginResponse(
            user.Id.ToString(),
            user.Email,
            true,
            token);
    }

    private async Task<User> CreateUserAsync(string provider, ExternalUserData externalUser)
    {
        var user = User.CreateFromExternalLogin(
            provider,
            externalUser.ExternalId,
            externalUser.Email,
            externalUser.Name,
            externalUser.Picture);

        await _userRepository.AddAsync(user);
        return user;
    }
}
