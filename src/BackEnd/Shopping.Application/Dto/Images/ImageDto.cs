using System.ComponentModel.DataAnnotations;

namespace Shopping.Application.Dto.Images;

public class ImageDto
{
    [Required] public string Base64 { get; init; } = null!;
    [Required] public string Extension { get; init; } = null!;
}