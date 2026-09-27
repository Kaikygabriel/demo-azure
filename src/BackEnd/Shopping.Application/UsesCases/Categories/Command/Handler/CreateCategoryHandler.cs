using Shopping.Application.Interfaces.Repositories;
using Shopping.Application.UsesCases.Categories.Command.Request;
using Shopping.Domain.BackOffice.Commum;
using Shopping.Domain.BackOffice.Entities;

namespace Shopping.Application.UsesCases.Categories.Command.Handler;

internal sealed class CreateCategoryHandler : IRequestHandler<CreateCategoryRequest,ResultValue<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICategoryRepository _categoryRepository;

    public CreateCategoryHandler(IUnitOfWork unitOfWork, ICategoryRepository categoryRepository)
    {
        _unitOfWork = unitOfWork;
        _categoryRepository = categoryRepository;
    }

    public async Task<ResultValue<Guid>> Handle(CreateCategoryRequest request, CancellationToken cancellationToken)
    {
        if (await _categoryRepository.ExistsByTitle(request.Title, cancellationToken))
            return new Error("Category Already Exists !");

        var category = new Category(request.Title);
        _categoryRepository.Create(category,cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);
        
        return category.Id;
    }
}