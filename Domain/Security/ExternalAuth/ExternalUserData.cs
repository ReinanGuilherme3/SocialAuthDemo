namespace Domain.Security.ExternalAuth;

public record ExternalUserData(
    string ExternalId,
    string Email,
    string Name,
    string? Picture
);