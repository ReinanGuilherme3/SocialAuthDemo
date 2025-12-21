namespace Domain.Security.ExternalAuth;

public interface IExternalAuthProvider
{
    string Name { get; }
    string GenerateLoginUrl();
    Task<ExternalUserData> AuthenticateAsync(string credential);
}
