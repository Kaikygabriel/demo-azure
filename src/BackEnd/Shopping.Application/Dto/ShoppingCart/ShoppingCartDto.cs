using Shopping.Application.Dto.Order;

namespace Shopping.Application.Dto.ShoppingCart;

public record ShoppingCartDto(Guid UserId, string Email, string Name, IEnumerable<OrderShoppingCartDto> Orders)
{
    public decimal GetTotal()
        => Orders.Sum(x => x.Total());
};