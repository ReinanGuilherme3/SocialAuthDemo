namespace Infrastructure.Security.ExternalAuth.GitHub;


public class GitHubUserResponse
{
    public long id { get; set; }
    public string login { get; set; } = null!;
    public string email { get; set; }
    public string name { get; set; }
    public string avatar_url { get; set; }
}