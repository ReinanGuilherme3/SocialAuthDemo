namespace Infrastructure.Security.ExternalAuth.Google;

public class GoogleOAuthSettings
{
    public string ClientId { get; init; } = default!;
    public string ClientSecret { get; init; } = default!;
    public string RedirectUri { get; init; } = default!;
}