using Shopping.Application.Dto.ShoppingCart;
using Shopping.Domain.BackOffice.Commum;

namespace Shoppping.web.Interfaces;

public interface IShoppingCartService
{
    Task<ResultValue<ShoppingCartDto>> GetByUserId(Guid userId);
}