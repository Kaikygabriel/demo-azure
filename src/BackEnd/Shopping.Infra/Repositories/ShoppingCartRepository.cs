using Microsoft.EntityFrameworkCore;
using Shopping.Application.Interfaces.Repositories;
using Shopping.Domain.BackOffice.Entities;
using Shopping.Infra.Data.Context;

namespace Shopping.Infra.Repositories;

internal sealed class ShoppingCartRepository : IShoppingCartRepository
{
    private readonly AppDbContext _dbContext;

    public ShoppingCartRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ShoppingCart?> GetByUserIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.ShoppingCarts
            .Include(x => x.User)
            .Include(x => x.Orders)
            .FirstOrDefaultAsync(x => x.UserId == id, cancellationToken);
    }

    public void Update(ShoppingCart shoppingCart)
    {
        _dbContext.ShoppingCarts.Update(shoppingCart);
    }
}