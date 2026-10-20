using Shopping.Application.Interfaces.Repositories;
using Shopping.Application.UsesCases.Comment.Command.Request;
using Shopping.Domain.BackOffice.Commum;

namespace Shopping.Application.UsesCases.Comment.Command.Handler;

internal sealed class RemoveCommentHandler : IRequestHandler<RemoveCommentRequest,Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductRepository _productRepository;
    private readonly ICommentRepository _commentRepository;

    public RemoveCommentHandler(IUnitOfWork unitOfWork, IProductRepository productRepository, ICommentRepository commentRepository)
    {
        _unitOfWork = unitOfWork;
        _productRepository = productRepository;
        _commentRepository = commentRepository;
    }

    public async Task<Result> Handle(RemoveCommentRequest request, CancellationToken cancellationToken)
    {
        var comment = await _commentRepository.GetById(request.CommentId,cancellationToken);
        if (comment is null || comment.UserId != request.UserId)
            return new Error("Comment not found!");

        var product = await _productRepository.GetById(comment.ProductId,cancellationToken);
        var result = product!.RemoveComment(comment);
        if (!result.IsSuccess)
            return result.Error;
        
        _productRepository.Update(product);
        _commentRepository.Delete(comment);

        await _unitOfWork.CommitAsync(cancellationToken);

        return Result.Success();
    }
}