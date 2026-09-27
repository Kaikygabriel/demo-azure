using System.ComponentModel.DataAnnotations;
using Shopping.Domain.BackOffice.Commum;
using Shopping.Domain.BackOffice.Entities;
using Shopping.Application.Dto.Images;

namespace Shopping.Application.UsesCases.Products.Commands.Request;

public record CreateProductRequest : IRequest<ResultValue<Guid>>
{
    [Required]
    public Guid CategoryId { get; init; }
    [Required]
    public string Title { get; init; } = null!;
    [Required]
    public string Summary { get; init; } = null!;
    [Required]
    public int Stock { get; init; }
    
    public ImageDto? Image { get; init; }
    [Required]
    public decimal Price { get; init; }

    public ResultValue<Product> ToEntity(Category category)
        => Product.Factory.Create(category, Price, Stock, 0, Title, Summary);
}
