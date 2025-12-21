namespace Infrastructure.Security.ExternalAuth.GitHub;

public class GitHubTokenResponse
{
    public string access_token { get; set; } = null!;
    public string token_type { get; set; } = null!;
}