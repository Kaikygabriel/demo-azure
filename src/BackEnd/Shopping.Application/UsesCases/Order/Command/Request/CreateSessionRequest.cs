using System.ComponentModel.DataAnnotations;
using Shopping.Domain.BackOffice.Commum;

namespace Shopping.Application.UsesCases.Order.Command.Request;

public record CreateSessionRequest : IRequest<ResultValue<string>>
{
    [Required]
    public Guid UserId { get; init; }
};