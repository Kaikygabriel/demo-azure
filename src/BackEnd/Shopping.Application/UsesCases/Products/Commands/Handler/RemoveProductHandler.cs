using Shopping.Application.Interfaces.Repositories;
using Shopping.Application.UsesCases.Products.Commands.Request;
using Shopping.Domain.BackOffice.Commum;

namespace Shopping.Application.UsesCases.Products.Commands.Handler;

internal sealed class RemoveProductHandler : IRequestHandler<RemoveProductRequest,Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductRepository _productRepository;

    public RemoveProductHandler(IUnitOfWork unitOfWork, IProductRepository productRepository)
    {
        _unitOfWork = unitOfWork;
        _productRepository = productRepository;
    }

    public async Task<Result> Handle(RemoveProductRequest request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetById(request.ProductId, cancellationToken);
        if (product is null)
            return new Error("Product not found!");
        
        _productRepository.Delete(product);
        await _unitOfWork.CommitAsync(cancellationToken);
        
        return Result.Success();
    }
}