using Microsoft.EntityFrameworkCore;
using Shopping.Application.Dto.Comments;
using Shopping.Application.Dto.Order;
using Shopping.Application.Dto.Products;
using Shopping.Application.Interfaces.Queries;
using Shopping.Infra.Data.Context;

namespace Shopping.Infra.Query;

internal sealed class OrderQuery : IOrderQuery
{
    private readonly AppDbContext _dbContext;

    public OrderQuery(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<OrderDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await _dbContext.Orders.Where(x=>x.Id == id).Select(x => 
                new OrderDto(x.Id,
                        new ProductDetailsDto
                            (x.Product.Id,x.Product.Category,x.Product.Title,x.Product.Summary,x.Product.ImageThumbUrl,x.Product.ImagesUrl,x.Product.Price ,x.Product.Discount,x.Product.Stock,x.Product.Comments.Select(x=> new CommentDto(x.Id,x.User.Name,x.Message,x.Stars))),
                        x.Voucher))
            .FirstOrDefaultAsync(cancellationToken);
}