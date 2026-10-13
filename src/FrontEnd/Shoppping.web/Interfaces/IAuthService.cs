using Shopping.Application.UsesCases.Users.Command.Request;
using Shopping.Application.UsesCases.Users.Command.Response;
using Shopping.Domain.BackOffice.Commum;

namespace Shoppping.web.Interfaces;

public interface IAuthService
{
    Task<ResultValue<AuthUserResponse>> LoginByRefreshTokenAsync(LoginByRefreshTokenRequest request,CancellationToken cancellation);
    
    Task<ResultValue<AuthUserResponse>> LoginAsync(LoginUserRequest request,CancellationToken cancellationToken);
    
    Task<ResultValue<AuthUserResponse>> RegisterAsync(RegisterUserRequest request,CancellationToken cancellationToken );
}