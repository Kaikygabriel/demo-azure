using Shopping.Application.Configurations;
using Shopping.Application.Interfaces.Repositories;
using Shopping.Application.Interfaces.Services;
using Shopping.Application.UsesCases.Users.Command.Request;
using Shopping.Application.UsesCases.Users.Command.Response;
using Shopping.Domain.BackOffice.Commum;
using Shopping.Domain.BackOffice.ValueObjects;

namespace Shopping.Application.UsesCases.Users.Command.Handler;

internal sealed class RegisterUserHandler : IRequestHandler<RegisterUserRequest,ResultValue<AuthUserResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenService _tokenService;
    
    public RegisterUserHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
    }

    public async Task<ResultValue<AuthUserResponse>> Handle(RegisterUserRequest request, CancellationToken cancellationToken)
    {
        if (await _userRepository.EmailInUse(request.Email, cancellationToken))
            return new Error("Email in use!");

        var resultCreateUser = request.ToEntity();
        if (!resultCreateUser.IsSuccess)
            return resultCreateUser.Error;

        var user = resultCreateUser.Value;
        var token = _tokenService.GenerateAccessToken(user,cancellationToken);
        var refreshToken =new RefreshToken(_tokenService.GenerateRefreshToken(),DateTime.UtcNow.AddHours(JwtConfiguration.ExpiredRefreshTokenInHours));
        user.SetRefreshToken(refreshToken);
        
        _userRepository.Create(user);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new AuthUserResponse(
            user.Id,
            token,
            refreshToken.Code,
            DateTime.UtcNow.AddHours(JwtConfiguration.ExpiredTokenInHours),
            DateTime.UtcNow.AddHours(JwtConfiguration.ExpiredRefreshTokenInHours));
    }
}