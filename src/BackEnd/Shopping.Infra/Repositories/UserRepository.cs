using Microsoft.EntityFrameworkCore;
using Shopping.Application.Interfaces.Repositories;
using Shopping.Domain.BackOffice.Entities;
using Shopping.Infra.Data.Context;

namespace Shopping.Infra.Repositories;

internal sealed class UserRepository : IUserRepository
{
    private readonly AppDbContext _dbContext;

    public UserRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> EmailInUse(string email, CancellationToken cancellationToken)
    {
        return await _dbContext.Users.AnyAsync(x => x.Email.Address == email,cancellationToken);
    }

    public async Task<User?> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<User?> GetByEmail(string email, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(x => x.Email.Address == email, cancellationToken);
    }

    public void Create(User user)
    {
        _dbContext.Users.Add(user);
    }

    public void Update(User user)
    {
        _dbContext.Users.Update(user);
    }
}