using Shopping.Domain.BackOffice.Entities;

namespace Shopping.Application.Interfaces.Repositories;

public interface IShoppingCartRepository
{
    Task<ShoppingCart?> GetByUserIdAsync(Guid id, CancellationToken cancellationToken = default);
    void Update(ShoppingCart shoppingCart);
}