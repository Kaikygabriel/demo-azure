using System.ComponentModel.DataAnnotations;
using Shopping.Domain.BackOffice.Commum;

namespace Shopping.Application.UsesCases.Products.Commands.Request;

public record RemoveProductRequest : IRequest<Result>
{
    [Required]
    public Guid ProductId { get; init; }
}