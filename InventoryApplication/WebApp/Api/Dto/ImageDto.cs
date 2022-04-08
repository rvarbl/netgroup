using System.ComponentModel.DataAnnotations;

namespace WebApp.Api.Dto;

public class ImageDto
{
    public string? Path { get; set; } = default!;
    public IFormFile? Image { get; set; }
    public Guid? StorageItemId { get; set; }
}