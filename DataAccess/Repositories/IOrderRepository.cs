using BussinessObject.Entities;

namespace DataAccess.Repositories;

public interface IOrderRepository
{
    Task<List<Order>> GetOrderAsync();
    Task<Order?> GetOrderByIdAsync(int orderId);
    Task<List<Order>> GetOrdersByMemberIdAsync(string memberId);
    Task AddOrderAsync(Order order);
    Task UpdateOrderAsync(Order order);
    Task DeleteOrderAsync(int orderId);
    Task AddOrderDetails(List<OrderDetail> orderDetails);
}
