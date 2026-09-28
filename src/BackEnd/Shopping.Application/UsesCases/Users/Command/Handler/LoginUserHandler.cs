using Shopping.Application.Configurations;
using Shopping.Application.Interfaces.Repositories;
using Shopping.Application.Interfaces.Services;
using Shopping.Application.UsesCases.Users.Command.Request;
using Shopping.Application.UsesCases.Users.Command.Response;
using Shopping.Domain.BackOffice.Commum;
using Shopping.Domain.BackOffice.ValueObjects;

namespace Shopping.Application.UsesCases.Users.Command.Handler;

internal sealed class LoginUserHandler : IRequestHandler<LoginUserRequest,ResultValue<AuthUserResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenService _tokenService;

    public LoginUserHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
    }

    public async Task<ResultValue<AuthUserResponse>> Handle(LoginUserRequest request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmail(request.Email, cancellationToken);
        if (user is null)
            return new Error("User not Found !");
        
        if(!user.Password.Verify(request.Password))
            return new Error("Password invalid!");
        
        var token = _tokenService.GenerateAccessToken(user,cancellationToken);
        var refreshToken =new RefreshToken(_tokenService.GenerateRefreshToken(),DateTime.UtcNow.AddHours(JwtConfiguration.ExpiredRefreshTokenInHours));
        user.SetRefreshToken(refreshToken);
        
        _userRepository.Update(user);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new AuthUserResponse(
            user.Id,
            token,
            refreshToken.Code,
            DateTime.UtcNow.AddHours(JwtConfiguration.ExpiredTokenInHours),
            DateTime.UtcNow.AddHours(JwtConfiguration.ExpiredRefreshTokenInHours));
    }
}