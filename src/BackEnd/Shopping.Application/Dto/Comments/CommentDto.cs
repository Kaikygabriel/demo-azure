namespace Shopping.Application.Dto.Comments;

public sealed record CommentDto(Guid Id,string UserName,string Message,byte Stars);