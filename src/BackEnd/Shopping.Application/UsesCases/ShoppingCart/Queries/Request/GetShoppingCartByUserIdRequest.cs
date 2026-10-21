using Shopping.Application.Dto.ShoppingCart;
using Shopping.Domain.BackOffice.Commum;

namespace Shopping.Application.UsesCases.ShoppingCart.Queries.Request;

public record GetShoppingCartByUserIdRequest(Guid UserId) : IRequest<ResultValue<ShoppingCartDto>>;