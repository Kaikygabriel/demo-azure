using Shopping.Domain.BackOffice.Entities;

namespace Shopping.Application.Dto.Products;

public record ProductDetailsDto(Guid Id,Category Category,string Title,string Summary,string? ImageThumb,List<string> Images,decimal Price,decimal Discount,int Stock);