using Shopping.Application.UsesCases.Users.Command.Response;
using Shopping.Domain.BackOffice.Commum;

namespace Shopping.Application.UsesCases.Users.Command.Request;

 public record LoginUserRequest(string Email,string Password) : IRequest<ResultValue<AuthUserResponse>>;