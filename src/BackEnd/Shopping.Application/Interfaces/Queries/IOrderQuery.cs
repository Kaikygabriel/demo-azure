using Shopping.Application.Dto.Order;

namespace Shopping.Application.Interfaces.Queries;

public interface IOrderQuery
{
    Task<OrderDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}