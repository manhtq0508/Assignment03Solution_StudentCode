using BussinessObject.Entities;
using DataAccess.DAO;

namespace DataAccess.Repositories;

public class OrderRepository : IOrderRepository
{
    public Task<List<Order>> GetOrderAsync() => OrderDAO.GetOrdersAsync();
    public Task<Order?> GetOrderByIdAsync(int orderId) => OrderDAO.GetOrderByIdAsync(orderId);
    public Task<List<Order>> GetOrdersByMemberIdAsync(string memberId) => OrderDAO.GetOrdersByMemberIdAsync(memberId);
    public Task AddOrderAsync(Order order) => OrderDAO.AddOrderAsync(order);
    public Task UpdateOrderAsync(Order order) => OrderDAO.UpdateOrderAsync(order);
    public Task DeleteOrderAsync(int orderId) => OrderDAO.DeleteOrderAsync(orderId);
}
