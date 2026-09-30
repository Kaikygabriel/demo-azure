using Microsoft.AspNetCore.Mvc;
using MediatR;
using Shopping.Application.UsesCases.Products.Commands.Request;
using Shopping.Application.UsesCases.Products.Queries.Request;

namespace Shopping.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ISender _sender;

    public ProductsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateProductRequest request,CancellationToken cancellationToken)
    {
        var result = await _sender.Send(request, cancellationToken);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
    
    [HttpGet]
    public async Task<ActionResult> GetAll([FromQuery] GetAllProductsRequest request,CancellationToken cancellationToken)
    {
        var result = await _sender.Send(request, cancellationToken);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpGet("By")]
    public async Task<ActionResult> GetById([FromQuery]GetProductByIdRequest request,CancellationToken cancellationToken)
    {
        var result = await _sender.Send(request, cancellationToken);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
}