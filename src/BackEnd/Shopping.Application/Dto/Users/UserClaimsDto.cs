namespace Shopping.Application.Dto.Users;

public record UserClaimsDto(Guid Id,string Email,List<string> Role);