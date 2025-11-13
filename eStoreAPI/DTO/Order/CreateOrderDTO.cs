using System.ComponentModel.DataAnnotations;

namespace eStoreAPI.DTO.Order;

public class CreateOrderDTO
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "MemberId is required")]
    public string MemberId { get; set; } = string.Empty;

    [Required(ErrorMessage = "Freight is required")]
    [Range(0, double.MaxValue, ErrorMessage = "Freight must be a non-negative value")]
    public decimal Freight { get; set; }

    [Required(ErrorMessage = "OrderDetails is required")]
    [MinLength(1, ErrorMessage = "At least one OrderDetails is required")]
    public List<CreateOrderDetailDTO> OrderDetails { get; set; } = new List<CreateOrderDetailDTO>();
}
