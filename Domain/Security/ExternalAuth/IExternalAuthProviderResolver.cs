namespace Domain.Security.ExternalAuth;

public interface IExternalAuthProviderResolver
{
    IExternalAuthProvider Resolve(string provider);
}
