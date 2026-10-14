using System.ComponentModel.DataAnnotations;
using Shopping.Application.UsesCases.Users.Command.Response;
using Shopping.Domain.BackOffice.Commum;

namespace Shopping.Application.UsesCases.Users.Command.Request;

public record LoginUserRequest : IRequest<ResultValue<AuthUserResponse>>
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}