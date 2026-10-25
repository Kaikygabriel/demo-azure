using Microsoft.EntityFrameworkCore;
using Shopping.Application.Dto.Comments;
using Shopping.Application.Dto.Order;
using Shopping.Application.Dto.Products;
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
                    x.Orders.Select(x=>new OrderShoppingCartDto(x.Id,
                        new ProductShoppingCartDetailsDto
                            (x.Product.Id,x.Product.Title,x.Product.Summary,x.Product.ImageThumbUrl,x.Product.Price ,x.Product.Discount,x.Product.Stock,x.Product.Stars,x.Product.Comments.Count),
                        x.Voucher))))
            .FirstOrDefaultAsync(cancellationToken);
    }
}