using System.ComponentModel.DataAnnotations;
using Shopping.Domain.BackOffice.Commum;

namespace Shopping.Application.UsesCases.Comment.Command.Request;

public record CreateCommentRequest : IRequest<Result>
{
    [Required]
    public Guid ProductId { get; init; }
    [Required]
    public Guid UserId { get; init; }

    [Required]
    public string Message { get; init; } = null!;
    [Required]
    [Range(0,5)]
    public byte Stars { get; init; }
};