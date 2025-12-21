using Domain.Security.ExternalAuth;

namespace Infrastructure.Security.ExternalAuth;

internal class ExternalAuthProviderResolver : IExternalAuthProviderResolver
{
    private readonly IEnumerable<IExternalAuthProvider> _providers;

    public ExternalAuthProviderResolver(IEnumerable<IExternalAuthProvider> providers)
    {
        _providers = providers;
    }

    public IExternalAuthProvider Resolve(string provider)
    {
        var resolved = _providers.FirstOrDefault(p =>
            p.Name.Equals(provider, StringComparison.OrdinalIgnoreCase));

        if (resolved is null)
            throw new InvalidOperationException($"Auth provider '{provider}' not supported.");

        return resolved;
    }
}