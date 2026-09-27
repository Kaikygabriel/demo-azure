using Shopping.Application.Interfaces.Repositories;
using Shopping.Application.Interfaces.Services;
using Shopping.Application.UsesCases.Products.Commands.Request;
using Shopping.Domain.BackOffice.Commum;

namespace Shopping.Application.UsesCases.Products.Commands.Handler;

internal sealed class AddImageInProductHandler : IRequestHandler<AddImageInProductRequest,ResultValue<Guid>>
{
    private readonly IStorageService _storageService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductRepository _productRepository;

    public AddImageInProductHandler(IStorageService storageService, IUnitOfWork unitOfWork, IProductRepository productRepository)
    {
        _storageService = storageService;
        _unitOfWork = unitOfWork;
        _productRepository = productRepository;
    }

    public async Task<ResultValue<Guid>> Handle(AddImageInProductRequest request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetById(request.Id, cancellationToken);
        if (product is null)
            return new Error("Product not found!");

        var resultAddImageInStorage = await _storageService.Insert(request.Image.Base64, request.Image.Extension);
        if (!resultAddImageInStorage.IsSuccess)
            return resultAddImageInStorage.Error;
        
        var result = product.AddImage(resultAddImageInStorage.Value);
        if (!result.IsSuccess)
            return result.Error;
        
        _productRepository.Update(product);
        await _unitOfWork.CommitAsync(cancellationToken);
        
        return product.Id;
    }
}