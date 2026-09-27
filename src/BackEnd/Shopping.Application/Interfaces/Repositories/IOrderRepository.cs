using Shopping.Domain.BackOffice.Entities;

namespace Shopping.Application.Interfaces.Repositories;

public interface IOrderRepository
{
    Task<Order?> GetById(Guid id, CancellationToken cancellationToken = default);

    void Create(Order order);
    void Update(Order order);
    void Delete(Order order);
}