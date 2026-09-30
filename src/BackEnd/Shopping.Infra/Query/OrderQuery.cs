using Microsoft.EntityFrameworkCore;
using Shopping.Application.Dto.Order;
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
        => await _dbContext.Orders.Where(x=>x.Id == id).Select(x=>new OrderDto(x.Id,x.Product,x.Voucher,x.UserId,x.User.Email.Address)).FirstOrDefaultAsync(cancellationToken);
}