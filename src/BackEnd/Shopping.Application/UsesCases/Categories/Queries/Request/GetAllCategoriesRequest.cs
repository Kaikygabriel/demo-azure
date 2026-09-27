using Shopping.Application.Commum;
using Shopping.Domain.BackOffice.Commum;
using Shopping.Domain.BackOffice.Entities;

namespace Shopping.Application.UsesCases.Categories.Queries.Request;

public record GetAllCategoriesRequest : IRequest<ResultValue<PagedResponse<List<Category>>>>
{
    public int Page { get; init;}
    public int PageSize { get; init;}
}