using Shopping.Domain.BackOffice.Entities;

namespace Shopping.Application.Dto.Products;

public record ProductDto(Guid Id,string Title,string Summary,string? ImageThumb,decimal Price,decimal Discount,string Category);