using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shopping.Application.UsesCases.Categories.Command.Request;
using Shopping.Application.UsesCases.Categories.Queries.Request;

namespace Shopping.Api.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class CategoriesController : ControllerBase
{
    private readonly ISender _sender;

    public CategoriesController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateCategoryRequest request, CancellationToken cancellation)
    {
        var result = await _sender.Send(request, cancellation);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpGet]
    public async Task<ActionResult> GetAll([FromQuery] GetAllCategoriesRequest request, CancellationToken cancellation)
    {
        var result = await _sender.Send(request, cancellation);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
}