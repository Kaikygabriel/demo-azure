using Shopping.Application.Interfaces.Repositories;
using Shopping.Application.Interfaces.Services;
using Shopping.Application.UsesCases.Order.Command.Request;
using Shopping.Domain.BackOffice.Commum;

namespace Shopping.Application.UsesCases.Order.Command.Handler;

internal sealed class CreateSessionHandler: IRequestHandler<CreateSessionRequest,ResultValue<string>>
{

    private readonly IShoppingCartRepository _shoppingCartRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPayService _payService;

    public CreateSessionHandler( IUnitOfWork unitOfWork, IPayService payService, IShoppingCartRepository shoppingCartRepository)
    {
    
        _unitOfWork = unitOfWork;
        _payService = payService;
        _shoppingCartRepository = shoppingCartRepository;
    }

    public async Task<ResultValue<string>> Handle(CreateSessionRequest request, CancellationToken cancellationToken)
    {
        var cart = await _shoppingCartRepository.GetByUserIdAsync(request.UserId, cancellationToken);
        if(cart is null)
            return new Error("Cart not Found!");
        
        var result = await _payService.CreateSession(cart, cancellationToken);
        if (!result.IsSuccess)
            return result.Error;
        
        cart.AlterUri(result.Value);
        _shoppingCartRepository.Update(cart);

        await _unitOfWork.CommitAsync(cancellationToken);

        return result.Value;
    }
}