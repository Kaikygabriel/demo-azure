using Shopping.Application.UsesCases.Categories.Queries.Request;
using Shopping.Domain.BackOffice.Commum;
using Shopping.Domain.BackOffice.Entities;
using Shopping.Application.Commum;
using Shopping.Application.Interfaces.Repositories;

namespace Shopping.Application.UsesCases.Categories.Queries.Handler;

internal sealed class GetAllCategoryHandler : IRequestHandler<GetAllCategoriesRequest,ResultValue<PagedResponse<List<Category>>>>
{
    private readonly ICategoryRepository _categoryRepository;

    public GetAllCategoryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<ResultValue<PagedResponse<List<Category>>>> Handle(GetAllCategoriesRequest request, CancellationToken cancellationToken)
    {
        var categories = await _categoryRepository.GetAll(request.Page, request.PageSize, cancellationToken);
        var total = await _categoryRepository.GetTotal(cancellationToken);

        return new PagedResponse<List<Category>>(request.Page,request.PageSize,categories,total);
    }
}