using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shopping.Application.UsesCases.Order.Command.Request;
using Shopping.Application.UsesCases.Order.Queries.Request;

namespace Shopping.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
    private readonly ISender _sender;

    public OrdersController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateOrderRequest request,CancellationToken cancellationToken)
    {
        var result = await _sender.Send(request,cancellationToken);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpGet]
    public async Task<ActionResult> GetById([FromQuery] GetOrderByIdRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(request,cancellationToken);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
}