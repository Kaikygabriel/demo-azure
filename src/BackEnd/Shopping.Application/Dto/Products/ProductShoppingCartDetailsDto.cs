namespace Shopping.Application.Dto.Products;

public record ProductShoppingCartDetailsDto(Guid Id,string Title,string Summary,string? ImageThumb,decimal Price,decimal Discount,int Stock,int Stars,int CommentCount);