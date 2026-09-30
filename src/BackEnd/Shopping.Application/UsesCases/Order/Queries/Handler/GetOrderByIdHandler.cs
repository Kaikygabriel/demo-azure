using Shopping.Application.Dto.Order;
using Shopping.Application.Interfaces.Queries;
using Shopping.Application.UsesCases.Order.Queries.Request;
using Shopping.Domain.BackOffice.Commum;

namespace Shopping.Application.UsesCases.Order.Queries.Handler;

internal sealed class GetOrderByIdHandler : IRequestHandler<GetOrderByIdRequest,ResultValue<OrderDto>>
{
    private readonly IOrderQuery _orderQuery;

    public GetOrderByIdHandler(IOrderQuery orderQuery)
    {
        _orderQuery = orderQuery;
    }

    public async Task<ResultValue<OrderDto>> Handle(GetOrderByIdRequest request, CancellationToken cancellationToken)
    {
        var result = await _orderQuery.GetByIdAsync(request.Id,cancellationToken);
        return result is null ? new Error("Order not found") : result;
    }
}