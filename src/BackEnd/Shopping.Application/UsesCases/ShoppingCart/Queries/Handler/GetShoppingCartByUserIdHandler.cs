using Shopping.Application.Dto.ShoppingCart;
using Shopping.Application.Interfaces.Queries;
using Shopping.Application.UsesCases.ShoppingCart.Queries.Request;
using Shopping.Domain.BackOffice.Commum;

namespace Shopping.Application.UsesCases.ShoppingCart.Queries.Handler;

internal sealed class GetShoppingCartByUserIdHandler : IRequestHandler<GetShoppingCartByUserIdRequest,ResultValue<ShoppingCartDto>>
{
    private readonly IShoppingCartQuery _shoppingCartQuery;

    public GetShoppingCartByUserIdHandler(IShoppingCartQuery shoppingCartQuery)
    {
        _shoppingCartQuery = shoppingCartQuery;
    }

    public async Task<ResultValue<ShoppingCartDto>> Handle(GetShoppingCartByUserIdRequest request, CancellationToken cancellationToken)
    {
        var result =  await _shoppingCartQuery.GetByUserIdAsync(request.UserId, cancellationToken);
        if (result is null)
            return new Error("Not Found !");

        return result;
    }
}