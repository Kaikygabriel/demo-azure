using System.ComponentModel.DataAnnotations;
using Shopping.Application.Dto.Images;
using Shopping.Domain.BackOffice.Commum;

namespace Shopping.Application.UsesCases.Products.Commands.Request;

public record AddImageInProductRequest  : IRequest<ResultValue<Guid>>
{
    [Required]
    public ImageDto Image { get; init; } = null!;
    [Required]
    public Guid Id{ get; init; } 
};