using Shopping.Application.Dto.Comments;
using Shopping.Domain.BackOffice.Entities;

namespace Shopping.Application.Dto.Products;

public record ProductDetailsDto(Guid Id,Category Category,string Title,string Summary,string? ImageThumb,IReadOnlyCollection<string> Images,decimal Price,decimal Discount,int Stock,IEnumerable<CommentDto> Comments);