using Shopping.Application.Interfaces.Repositories;
using Shopping.Application.Interfaces.Services;
using Shopping.Application.UsesCases.Order.Command.Request;
using Shopping.Domain.BackOffice.Commum;
using Shopping.Domain.BackOffice.Enum;

namespace Shopping.Application.UsesCases.Order.Command.Handler;

internal sealed class AlterStatusOrderHandler : IRequestHandler<AlterStatusOrderRequest,ResultValue<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IOrderRepository _orderRepository;
    private readonly IPayService _payService;

    public AlterStatusOrderHandler(IUnitOfWork unitOfWork, IOrderRepository orderRepository, IPayService payService)
    {
        _unitOfWork = unitOfWork;
        _orderRepository = orderRepository;
        _payService = payService;
    }

    public async Task<ResultValue<Guid>> Handle(AlterStatusOrderRequest request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetById(request.OrderId, cancellationToken);
        if(order is null)
            return new Error("Order not found");

        var transactions = await _payService.GetTransactions(request.OrderId, cancellationToken);
        if (!transactions.IsSuccess)
            return transactions.Error;

        if (transactions.Value.Any(x => x.Refound))
            order.AlterState(EStatePayment.Refound);
        
        if (transactions.Value.Any(x => x.Paid) && !transactions.Value.Any(x => x.Refound))
            order.AlterState(EStatePayment.Paid);

        _orderRepository.Update(order);
        await _unitOfWork.CommitAsync(cancellationToken);
        
        return order.Id;
    }
}