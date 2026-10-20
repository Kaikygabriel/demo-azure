using System.ComponentModel.DataAnnotations;
using Shopping.Domain.BackOffice.Commum;

namespace Shopping.Application.UsesCases.Comment.Command.Request;

public record RemoveCommentRequest : IRequest<Result>
{
    [Required]
    public Guid UserId { get; set; }
    [Required]
    public Guid CommentId { get; set; }
}