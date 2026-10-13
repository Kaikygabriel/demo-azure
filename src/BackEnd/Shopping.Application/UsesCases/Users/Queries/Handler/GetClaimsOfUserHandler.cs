using Shopping.Application.Dto.Users;
using Shopping.Application.Interfaces.Repositories;
using Shopping.Application.UsesCases.Users.Queries.Request;
using Shopping.Domain.BackOffice.Commum;

namespace Shopping.Application.UsesCases.Users.Queries.Handler;

internal sealed class GetClaimsOfUserHandler : IRequestHandler<GetClaimsOfUserRequest,ResultValue<UserClaimsDto>>
{
    private readonly IUserRepository _userRepository;

    public GetClaimsOfUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ResultValue<UserClaimsDto>> Handle(GetClaimsOfUserRequest request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetById(request.UserId,cancellationToken);
        if (user is null)
            return new Error("User not found !");

        return new UserClaimsDto(user.Id, user.Email.Address, [.. user.Roles.Select(x => x.Title)]);
    }
}