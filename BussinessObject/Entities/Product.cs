namespace BussinessObject.Entities;

public class Product
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public float Weight { get; set; }
    public decimal UnitPrice { get; set; }
    public int UnitsInStock { get; set; }

    public Category Category { get; set; } = null!;
}
