using Shopping.Domain.BackOffice.Entities;

namespace Shopping.Application.Interfaces.Services;

public interface ITokenService
{
    string GenerateAccessToken(User user, CancellationToken cancellationToken = default);
    string GenerateRefreshToken();
}