namespace Infrastructure.Security.ExternalAuth.Google;

public class GoogleTokenResponse
{
    public string access_token { get; set; } = default!;
    public string id_token { get; set; } = default!;
    public string refresh_token { get; set; } = default!;
    public int expires_in { get; set; }
    public string scope { get; set; } = default!;
    public string token_type { get; set; } = default!;
}
