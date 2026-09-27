using Shopping.Application.Interfaces.Repositories;
using Shopping.Application.Interfaces.Services;
using Shopping.Application.UsesCases.Order.Command.Request;
using Shopping.Domain.BackOffice.Commum;

namespace Shopping.Application.UsesCases.Order.Command.Handler;

internal sealed class CreateSessionHandler: IRequestHandler<CreateSessionRequest,ResultValue<string>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPayService _payService;

    public CreateSessionHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork, IPayService payService)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
        _payService = payService;
    }

    public async Task<ResultValue<string>> Handle(CreateSessionRequest request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetById(request.OrderId, cancellationToken);
        if(order is null)
            return new Error("Order not Found!");

        var result = await _payService.CreateSession(order, cancellationToken);
        if (!result.IsSuccess)
            return result.Error;
        
        order.AlterUri(result.Value);
        _orderRepository.Update(order);

        await _unitOfWork.CommitAsync(cancellationToken);

        return result.Value;
    }
}