using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shopping.Application.UsesCases.Users.Command.Request;

namespace Shopping.Api.Controllers;

[ApiController]
[Route("Users")]
public class UsersController : ControllerBase
{
    private readonly ISender _sender;

    public UsersController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("Register")]
    public async Task<ActionResult> Register([FromBody] RegisterUserRequest request, CancellationToken cancellation = default)
    {
        var result = await _sender.Send(request,cancellation);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
    [HttpPost("Login")]
    public async Task<ActionResult> Login([FromBody] LoginUserRequest request, CancellationToken cancellation = default)
    {
        var result = await _sender.Send(request,cancellation);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
    [HttpPost("Refresh-Token")]
    public async Task<ActionResult> RefreshToken([FromBody] LoginByRefreshTokenRequest request, CancellationToken cancellation = default)
    {
        var result = await _sender.Send(request,cancellation);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
}