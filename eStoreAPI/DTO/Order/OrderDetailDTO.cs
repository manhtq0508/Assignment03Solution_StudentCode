using eStoreAPI.DTO.Product;

namespace eStoreAPI.DTO.Order;

public class OrderDetailDTO
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public float Discount { get; set; }

    public ProductDTO Product { get; set; } = null!;
}
