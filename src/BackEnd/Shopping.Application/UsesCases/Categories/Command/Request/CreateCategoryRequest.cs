using System.ComponentModel.DataAnnotations;
using Shopping.Domain.BackOffice.Commum;

namespace Shopping.Application.UsesCases.Categories.Command.Request;

public record CreateCategoryRequest : IRequest<ResultValue<Guid>>
{
     [Required]
     public string Title { get; init; } = null!;
}