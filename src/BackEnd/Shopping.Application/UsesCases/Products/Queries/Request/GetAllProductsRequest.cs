using System.ComponentModel.DataAnnotations;
using Shopping.Application.Commum;
using Shopping.Application.Dto.Products;
using Shopping.Domain.BackOffice.Commum;
using Shopping.Domain.BackOffice.Entities;

namespace Shopping.Application.UsesCases.Products.Queries.Request;

public record GetAllProductsRequest : IRequest<ResultValue<PagedResponse<List<ProductDto>>>>
{
    [Required]
    public int Page { get; init; }
    [Required]
    public int PageSize { get; init; }
    
    public Guid? UserId { get; init; }
}