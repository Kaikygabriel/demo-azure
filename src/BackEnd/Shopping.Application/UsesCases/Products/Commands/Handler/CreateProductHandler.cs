using Shopping.Application.Interfaces.Repositories;
using Shopping.Application.Interfaces.Services;
using Shopping.Application.UsesCases.Products.Commands.Request;
using Shopping.Domain.BackOffice.Commum;

namespace Shopping.Application.UsesCases.Products.Commands.Handler;

internal sealed class CreateProductHandler : IRequestHandler<CreateProductRequest,ResultValue<Guid>>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IStorageService _storageService;
    
    public CreateProductHandler(IProductRepository productRepository, IUnitOfWork unitOfWork, ICategoryRepository categoryRepository, IStorageService storageService)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        _categoryRepository = categoryRepository;
        _storageService = storageService;
    }

    public async Task<ResultValue<Guid>> Handle(CreateProductRequest request, CancellationToken cancellationToken)
    {
        if (await _productRepository.ExistsByTitle(request.Title, cancellationToken))
            return new Error("Title in Product Already Exists");

        var category = await _categoryRepository.GetByIdAsync(request.CategoryId, cancellationToken);
        if (category is null)
            return new Error("Category not found !");
        
        var resultCreateProduct = request.ToEntity(category);
        if (!resultCreateProduct.IsSuccess)
            return resultCreateProduct.Error;

        var product = resultCreateProduct.Value;

        if (request.Image is not null)
        {
            var resultAddImage = await _storageService.Insert(request.Image.Base64,request.Image.Extension); 
            if(!resultAddImage.IsSuccess)
                return resultAddImage.Error;
            
            product.SetImageThumb(resultAddImage.Value);
        }
        
        _productRepository.Create(product);
        await _unitOfWork.CommitAsync(cancellationToken);

        return product.Id;
    }
}