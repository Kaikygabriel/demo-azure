using Shopping.Application.UsesCases.Order.Command.Request;
using Shopping.Domain.BackOffice.Commum;
using Shopping.Domain.BackOffice.Entities;

namespace Shoppping.web.Interfaces;

public interface IOrderService
{
    Task<ResultValue<Order>> CreatedAsync(CreateOrderRequest request,
        CancellationToken cancellation = default);
}