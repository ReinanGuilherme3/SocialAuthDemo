using Refit;

namespace Infrastructure.Security.ExternalAuth.GitHub;

public interface IGitHubUserApi
{
    [Get("/user")]
    Task<ApiResponse<GitHubUserResponse>> GetUserAsync([Header("Authorization")] string authorization);
}