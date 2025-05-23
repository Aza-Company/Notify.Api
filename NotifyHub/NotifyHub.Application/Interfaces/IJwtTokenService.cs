using NotifyHub.Domain.Aggregates;

namespace NotifyHub.Application.Interfaces;

public interface IJwtTokenService
{
    string GenerateToken(User user);
}
