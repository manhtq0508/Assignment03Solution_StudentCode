using eStoreAPI.DTO.Category;

namespace eStoreAPI.DTO.Product;

public class ProductDTO
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public float Weight { get; set; }
    public decimal UnitPrice { get; set; }
    public int UnitsInStock { get; set; }

    public CategoryDTO Category { get; set; } = null!;
}
