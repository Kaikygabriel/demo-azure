using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shopping.Application.UsesCases.Comment.Command.Request;

namespace Shopping.Api.Controllers;

[ApiController]
[Route("[controller]/v1")]
public class CommentsController : ControllerBase
{
    private readonly ISender _sender;

    public CommentsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<ActionResult> Create(CreateCommentRequest request, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(request, cancellationToken);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
    
    [HttpDelete]
    public async Task<ActionResult> Delete([FromQuery]RemoveCommentRequest request, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(request, cancellationToken);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
}