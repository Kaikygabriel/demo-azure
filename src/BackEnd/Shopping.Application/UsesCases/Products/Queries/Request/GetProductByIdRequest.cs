using Shopping.Application.Dto.Products;
using Shopping.Domain.BackOffice.Commum;

namespace Shopping.Application.UsesCases.Products.Queries.Request;

public record GetProductByIdRequest : IRequest<ResultValue<ProductDetailsDto>>
{
    public Guid Id { get; init; }
}