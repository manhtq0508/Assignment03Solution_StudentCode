using BussinessObject;
using BussinessObject.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAO;

public static class ProductDAO
{
    public static async Task<List<Product>> GetProductsAsync()
    {
        using (var context = new AppDbContext())
        {
            return await context.Products
                .Include(p => p.Category)
                .ToListAsync();
        }
    }

    public static async Task<Product?> GetProductByIdAsync(int productId)
    {
        using (var context = new AppDbContext())
        {
            return await context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == productId);
        }
    }

    public static async Task AddProductAsync(Product product)
    {
        using (var context = new AppDbContext())
        {
            context.Products.Add(product);
            await context.SaveChangesAsync();
        }
    }

    public static async Task UpdateProductAsync(Product product)
    {
        using (var context = new AppDbContext())
        {
            var existingProduct = await context.Products.FindAsync(product.Id);
            if (existingProduct != null)
            {
                context.Entry(existingProduct).CurrentValues.SetValues(product);
                await context.SaveChangesAsync();
            }
        }
    }

    public static async Task DeleteProductAsync(int productId)
    {
        using (var context = new AppDbContext())
        {
            var product = await context.Products.FindAsync(productId);
            if (product != null)
            {
                context.Products.Remove(product);
                await context.SaveChangesAsync();
            }
        }
    }
}
