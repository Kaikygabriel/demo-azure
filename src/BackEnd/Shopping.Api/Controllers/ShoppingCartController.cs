using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shopping.Application.UsesCases.ShoppingCart.Queries.Request;

namespace Shopping.Api.Controllers;

[ApiController]
[Route(("[controller]/v1"))]
public sealed class ShoppingCartController : ControllerBase
{
    private readonly ISender _sender;

    public ShoppingCartController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<ActionResult> GetByUserIdAsync([FromQuery] GetShoppingCartByUserIdRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await _sender.Send(request, cancellationToken);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
}