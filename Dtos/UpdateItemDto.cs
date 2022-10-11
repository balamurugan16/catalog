using System.ComponentModel.DataAnnotations;

namespace Catalog.Dtos {
  public record UpdateItemDto {
    [Required]
    public string Name;
    [Required]
    [Range(1, 1000)]
    public decimal Price;
  }
}