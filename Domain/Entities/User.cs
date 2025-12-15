namespace Domain.Entities;

public class User
{
    public Guid Id { get; private set; }
    public string ExternalProvider { get; private set; } = default!;
    public string ExternalId { get; private set; } = default!;
    public string Email { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    public string? Picture { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private User() { }

    public static User CreateFromExternalLogin(
        string provider,
        string externalId,
        string email,
        string name,
        string? picture)
    {
        return new User
        {
            Id = Guid.NewGuid(),
            ExternalProvider = provider,
            ExternalId = externalId,
            Email = email,
            Name = name,
            Picture = picture,
            CreatedAt = DateTime.UtcNow
        };
    }
}