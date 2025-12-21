using Domain.Entities;

namespace Application.Contracts.Auth;

public interface IToken
{
    string GenerateToken(User user);
}
