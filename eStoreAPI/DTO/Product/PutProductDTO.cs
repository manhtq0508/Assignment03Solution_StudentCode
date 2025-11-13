using System.ComponentModel.DataAnnotations;

namespace eStoreAPI.DTO.Product;

public class PutProductDTO
{
    [Required(ErrorMessage = "CategoryId is required")]
    public int? CategoryId { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "ProductName is required")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Weight is required")]
    [Range(0, float.MaxValue, ErrorMessage = "Weight must be a non-negative value")]
    public float? Weight { get; set; }

    [Required(ErrorMessage = "UnitPrice is required")]
    [Range(0, double.MaxValue, ErrorMessage = "UnitPrice must be a non-negative value")]
    public decimal? UnitPrice { get; set; }

    [Required(ErrorMessage = "UnitsInStock is required")]
    [Range(0, int.MaxValue, ErrorMessage = "UnitsInStock must be a non-negative value")]
    public int? UnitsInStock { get; set; }
}
