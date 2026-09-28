using Microsoft.EntityFrameworkCore;
using Shopping.Application.Interfaces.Repositories;
using Shopping.Domain.BackOffice.Entities;
using Shopping.Infra.Data.Context;

namespace Shopping.Infra.Repositories;

internal sealed class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _appDbContext;

    public OrderRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Order?> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        return await _appDbContext.Orders.FirstOrDefaultAsync(x=>x.Id == id,cancellationToken);
    }

    public void Create(Order order)
    {
        _appDbContext.Orders.Add(order);
    }

    public void Update(Order order)
    {
        _appDbContext.Orders.Update(order);
    }

    public void Delete(Order order)
    {
        _appDbContext.Orders.Remove(order);
    }
}