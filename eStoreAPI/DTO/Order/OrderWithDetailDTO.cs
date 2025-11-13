namespace eStoreAPI.DTO.Order;

public class OrderWithDetailDTO
{
    public int Id { get; set; }
    public string MemberId { get; set; } = string.Empty;
    public DateTime OrderDate { get; set; }
    public DateTime? RequiredDate { get; set; }
    public DateTime? ShippedDate { get; set; }
    public decimal Freight { get; set; }

    public List<OrderDetailDTO> OrderDetails { get; set; } = new List<OrderDetailDTO>();
}
