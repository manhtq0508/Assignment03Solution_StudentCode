using System.ComponentModel.DataAnnotations;

namespace eStoreAPI.DTO.Order;

public class CreateOrderDetailDTO
{
    [Required(ErrorMessage = "ProductId is required")]
    [Range(1, int.MaxValue, ErrorMessage = "ProductId must be a positive integer")]
    public int? ProductId { get; set; }

    [Required(ErrorMessage = "UnitPrice is required")]
    [Range(0, double.MaxValue, ErrorMessage = "UnitPrice must be a non-negative value")]
    public decimal? UnitPrice { get; set; }

    [Required(ErrorMessage = "Quantity is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be a positive integer")]
    public int? Quantity { get; set; }

    [Required(ErrorMessage = "Discount is required")]
    [Range(0, 1, ErrorMessage = "Discount must be between 0 and 1")]
    public float? Discount { get; set; } = 0;
}
