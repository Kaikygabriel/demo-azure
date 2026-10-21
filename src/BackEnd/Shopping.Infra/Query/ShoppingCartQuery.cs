using Microsoft.EntityFrameworkCore;
using Shopping.Application.Dto.Order;
using Shopping.Application.Dto.ShoppingCart;
using Shopping.Application.Interfaces.Queries;
using Shopping.Infra.Data.Context;

namespace Shopping.Infra.Query;

internal sealed class ShoppingCartQuery : IShoppingCartQuery
{
    private readonly AppDbContext _appDbContext;

    public ShoppingCartQuery(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<ShoppingCartDto?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _appDbContext.ShoppingCarts
            .Where(x => x.UserId == userId)
            .Select(x => 
                new ShoppingCartDto(
                    x.UserId,
                    x.User.Email.Address,
                    x.User.Name,
                    x.Orders.Select(x=>new OrderDto(x.Id,x.Product,x.Voucher))))
            .FirstOrDefaultAsync(cancellationToken);
    }
}