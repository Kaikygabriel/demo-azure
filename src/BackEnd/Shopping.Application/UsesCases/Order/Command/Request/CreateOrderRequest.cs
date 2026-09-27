using System.ComponentModel.DataAnnotations;
using Shopping.Domain.BackOffice.Commum;

namespace Shopping.Application.UsesCases.Order.Command.Request;

public record CreateOrderRequest : IRequest<ResultValue<Guid>>
{
    [Required]
    public Guid ProductId { get; init; }
    [Required]
    public Guid UserId { get; init; }
    public Guid? VoucherId { get; init; }
}