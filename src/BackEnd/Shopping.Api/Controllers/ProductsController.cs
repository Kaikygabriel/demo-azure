using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Shopping.Api.Extensions;
using Shopping.Application.UsesCases.Products.Commands.Request;
using Shopping.Application.UsesCases.Products.Queries.Request;
using Shopping.Infra.Configurations;

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
    [RequestSizeLimit(1024 * 1024)] //1 mb
    //[Authorize(Policy.Admin)]
    public async Task<ActionResult> Create([FromBody] CreateProductRequest request,CancellationToken cancellationToken)
    {
        var result = await _sender.Send(request, cancellationToken);
        return result.IsSuccess ? Ok(result) : BadRequest(result.Error.ToProblemDetails());
    }
    
    //Products/Image
    // Image
    [HttpPost("Image")]
    [RequestSizeLimit(1024 * 900)] //900 kb
    //[Authorize(Policy.Admin)]
    public async Task<ActionResult> AddImage([FromBody] AddImageInProductRequest request,CancellationToken cancellationToken)
    {
        var result = await _sender.Send(request, cancellationToken);
        return result.IsSuccess ? Ok(result) : BadRequest(result.Error.ToProblemDetails());
    }
    
    [HttpGet]
    public async Task<ActionResult> GetAll([FromQuery] GetAllProductsRequest request,CancellationToken cancellationToken)
    {
        var result = await _sender.Send(request, cancellationToken);
        return result.IsSuccess ? Ok(result) : BadRequest(result.Error.ToProblemDetails());
    }

    [HttpGet("By")]
   // [Authorize]
    public async Task<ActionResult> GetById([FromQuery]GetProductByIdRequest request,CancellationToken cancellationToken)
    {
        var result = await _sender.Send(request, cancellationToken);
        return result.IsSuccess ? Ok(result) : BadRequest(result.Error.ToProblemDetails());
    }
}