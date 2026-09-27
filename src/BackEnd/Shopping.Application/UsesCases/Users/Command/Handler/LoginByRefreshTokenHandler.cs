using Shopping.Application.Configurations;
using Shopping.Application.Interfaces.Repositories;
using Shopping.Application.Interfaces.Services;
using Shopping.Application.UsesCases.Users.Command.Request;
using Shopping.Application.UsesCases.Users.Command.Response;
using Shopping.Domain.BackOffice.Commum;
using Shopping.Domain.BackOffice.ValueObjects;

namespace Shopping.Application.UsesCases.Users.Command.Handler;

internal sealed class LoginByRefreshTokenHandler : IRequestHandler<LoginByRefreshTokenRequest,ResultValue<AuthUserResponse>>
{
    private readonly ITokenService _tokenService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;

    public LoginByRefreshTokenHandler(ITokenService tokenService, IUnitOfWork unitOfWork, IUserRepository userRepository)
    {
        _tokenService = tokenService;
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
    }

    public async Task<ResultValue<AuthUserResponse>> Handle(LoginByRefreshTokenRequest request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetById(request.UserId, cancellationToken);
        if (user is null)
            return new Error("User Not Found !");

        if (!user.RefreshToken.IsValid(request.RefreshToken))
            return new Error("Refresh Token Invalid");

        var token = _tokenService.GenerateAccessToken(user);
        var refreshToken = new RefreshToken(_tokenService.GenerateRefreshToken(),
            DateTime.UtcNow.AddHours(JwtConfiguration.ExpiredRefreshTokenInHours));

        user.SetRefreshToken(refreshToken);
        _userRepository.Update(user);
        
        await _unitOfWork.CommitAsync(cancellationToken);
        return new AuthUserResponse(
            user.Id,
            token,
            refreshToken.Code,
            DateTime.UtcNow.AddHours(JwtConfiguration.ExpiredRefreshTokenInHours),
            DateTime.UtcNow.AddHours(JwtConfiguration.ExpiredTokenInHours)
            );
    }
}