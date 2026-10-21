using Shopping.Application.Dto.ShoppingCart;

namespace Shopping.Application.Interfaces.Queries;

public interface IShoppingCartQuery
{
    Task<ShoppingCartDto?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
}