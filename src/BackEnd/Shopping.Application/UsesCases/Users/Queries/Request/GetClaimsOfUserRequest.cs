using  Shopping.Application.Dto.Users;
using Shopping.Domain.BackOffice.Commum;

namespace Shopping.Application.UsesCases.Users.Queries.Request;

public record GetClaimsOfUserRequest(Guid UserId) : IRequest<ResultValue<UserClaimsDto>>;