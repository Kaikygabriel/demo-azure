using Shopping.Application.Interfaces.Repositories;
using Shopping.Application.UsesCases.Order.Command.Request;
using Shopping.Domain.BackOffice.Commum;
using Shopping.Domain.BackOffice.Entities;

namespace Shopping.Application.UsesCases.Order.Command.Handler;

internal sealed class CreateOrderHandler : IRequestHandler<CreateOrderRequest,ResultValue<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IOrderRepository _orderRepository;
    private readonly IUserRepository _userRepository;
    private readonly IProductRepository _productRepository;
    private readonly IVoucherRepository _voucherRepository;
    
    public CreateOrderHandler(IUnitOfWork unitOfWork, IOrderRepository orderRepository, IUserRepository userRepository, IProductRepository productRepository, IVoucherRepository voucherRepository)
    {
        _unitOfWork = unitOfWork;
        _orderRepository = orderRepository;
        _userRepository = userRepository;
        _productRepository = productRepository;
        _voucherRepository = voucherRepository;
    }

    public async Task<ResultValue<Guid>> Handle(CreateOrderRequest request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetById(request.UserId, cancellationToken);
        if (user is null)
            return new Error("User not Found");

        var product = await _productRepository.GetById(request.ProductId, cancellationToken);
        if (product is null)
            return new Error("Product not found!");

        Voucher? voucher = null;
        if (request.VoucherId is not null && product.Discount <= 0)
            voucher = await _voucherRepository.GetByIdAsync(request.VoucherId ?? Guid.Empty, cancellationToken);

        var order = new Domain.BackOffice.Entities.Order(user, product, voucher);
        
        _orderRepository.Create(order);
        await _unitOfWork.CommitAsync(cancellationToken);
        
        return order.Id;
    }
}