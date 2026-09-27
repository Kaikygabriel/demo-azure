namespace Shopping.Application.UsesCases.Users.Command.Response;

public sealed record AuthUserResponse(Guid Id,string Token,string RefreshToken,DateTime ExpiredToken,DateTime ExpiredRefreshToken);