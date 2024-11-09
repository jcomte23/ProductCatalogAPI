using System.ComponentModel.DataAnnotations;

namespace ProductCatalogAPI.DTOs.Requests;

public class CategoryCreateDto
{
    [Required(ErrorMessage = "The Name field is required.")]
    [MinLength(3, ErrorMessage = "The Name must be at least 3 characters long.")]
    [MaxLength(50, ErrorMessage = "The Name must not exceed 50 characters.")]
    public required string Name { get; set; }

    [Required(ErrorMessage = "The Description field is required.")]
    [MinLength(10, ErrorMessage = "The Description must be at least 10 characters long.")]
    [MaxLength(250, ErrorMessage = "The Description must not exceed 250 characters.")]
    public required string Description { get; set; }
}

