using Shopping.Application.UsesCases.Users.Command.Response;
using Shopping.Domain.BackOffice.Commum;

namespace Shopping.Application.UsesCases.Users.Command.Request;

public record LoginByRefreshTokenRequest(string RefreshToken,Guid UserId) : IRequest<ResultValue<AuthUserResponse>>;