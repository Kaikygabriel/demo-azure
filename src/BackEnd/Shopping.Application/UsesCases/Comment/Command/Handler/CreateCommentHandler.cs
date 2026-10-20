using Shopping.Application.Interfaces.Repositories;
using Shopping.Application.UsesCases.Comment.Command.Request;
using Shopping.Domain.BackOffice.Commum;

namespace Shopping.Application.UsesCases.Comment.Command.Handler;

internal sealed class CreateCommentHandler : IRequestHandler<CreateCommentRequest,Result>
{
    private readonly IUserRepository _userRepository;
    private readonly IProductRepository _productRepository;
    private readonly ICommentRepository _commentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCommentHandler(ICommentRepository commentRepository, IUnitOfWork unitOfWork, IProductRepository productRepository, IUserRepository userRepository)
    {
        _commentRepository = commentRepository;
        _unitOfWork = unitOfWork;
        _productRepository = productRepository;
        _userRepository = userRepository;
    }

    public async Task<Result> Handle(CreateCommentRequest request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetById(request.UserId,cancellationToken);
        if (user is null)
            return new Error("User invalid!");
        var product = await _productRepository.GetById(request.ProductId, cancellationToken);
        if(product is null)
            return new Error("Product invalid!");

        var resultCreateMessage =
            Domain.BackOffice.Entities.Comment.Factory.Create(request.Stars, product, user, request.Message);

        if (!resultCreateMessage.IsSuccess)
            return resultCreateMessage.Error;

        var comment = resultCreateMessage.Value;
        var resultAddInProduct = product.AddComment(comment);

        if (!resultAddInProduct.IsSuccess)
            return resultAddInProduct.Error;

        _productRepository.Update(product);
        _commentRepository.Create(comment);
        await _unitOfWork.CommitAsync(cancellationToken);
        
        return Result.Success();
    }
}