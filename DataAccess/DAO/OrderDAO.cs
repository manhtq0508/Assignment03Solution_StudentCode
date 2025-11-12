using BussinessObject;
using BussinessObject.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAO;

public static class OrderDAO
{
    public static async Task<List<Order>> GetOrdersAsync()
    {
        using (var context = new AppDbContext())
        {
            return await context.Orders
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                        .ThenInclude(p => p.Category)
                .ToListAsync();
        }
    }

    public static async Task<Order?> GetOrderByIdAsync(int orderId)
    {
        using (var context = new AppDbContext())
        {
            return await context.Orders
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                        .ThenInclude(p => p.Category)
                .FirstOrDefaultAsync(o => o.Id == orderId);
        }
    }

    public static async Task<List<Order>> GetOrdersByMemberIdAsync(string memberId)
    {
        using (var context = new AppDbContext())
        {
            return await context.Orders
                .Where(o => o.MemberId == memberId)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                        .ThenInclude(p => p.Category)
                .ToListAsync();
        }
    }

    public static async Task AddOrderAsync(Order order)
    {
        using (var context = new AppDbContext())
        {
            context.Orders.Add(order);
            await context.SaveChangesAsync();
        }
    }

    public static async Task UpdateOrderAsync(Order order)
    {
        using (var context = new AppDbContext())
        {
            var existingOrder = await context.Orders.FindAsync(order.Id);
            if (existingOrder != null)
            {
                context.Entry(existingOrder).CurrentValues.SetValues(order);
                await context.SaveChangesAsync();
            }
        }
    }

    public static async Task DeleteOrderAsync(int orderId)
    {
        using (var context = new AppDbContext())
        {
            var order = await context.Orders.FindAsync(orderId);
            if (order != null)
            {
                context.Orders.Remove(order);
                await context.SaveChangesAsync();
            }
        }
    }

    public static async Task AddOrderDetails(List<OrderDetail> orderDetails)
    {
        using (var context = new AppDbContext())
        {
            context.OrderDetails.AddRange(orderDetails);
            await context.SaveChangesAsync();
        }
    }
}
