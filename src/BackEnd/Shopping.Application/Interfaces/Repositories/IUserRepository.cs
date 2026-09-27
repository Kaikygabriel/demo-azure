using Shopping.Domain.BackOffice.Entities;

namespace Shopping.Application.Interfaces.Repositories;

public interface IUserRepository
{
    Task<bool> EmailInUse(string email, CancellationToken cancellationToken);
    Task<User?> GetById(Guid id, CancellationToken cancellationToken = default);
    Task<User?> GetByEmail(string email, CancellationToken cancellationToken = default);
    void Create(User user);
    void Update(User user);
}