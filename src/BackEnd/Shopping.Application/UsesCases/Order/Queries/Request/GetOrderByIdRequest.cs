using System.ComponentModel.DataAnnotations;
using Shopping.Application.Dto.Order;
using Shopping.Domain.BackOffice.Commum;

namespace Shopping.Application.UsesCases.Order.Queries.Request;

public record GetOrderByIdRequest : IRequest<ResultValue<OrderDto>>
{
    [Required] public Guid Id { get; init; }
    public Guid UserId { get; init; }
}