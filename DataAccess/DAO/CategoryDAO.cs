using BussinessObject;
using BussinessObject.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAO;

public static class CategoryDAO
{
    public static async Task<List<Category>> GetcategoriesAsync()
    {
        using (var context = new AppDbContext())
        {
            return await context.Categories.ToListAsync();
        }
    }

    public static async Task<Category?> GetCategoryByIdAsync(int categoryId)
    {
        using (var context = new AppDbContext())
        {
            return await context.Categories.FindAsync(categoryId);
        }
    }

    public static async Task AddCategoryAsync(Category category)
    {
        using (var context = new AppDbContext())
        {
            context.Categories.Add(category);
            await context.SaveChangesAsync();
        }
    }

    public static async Task UpdateCategoryAsync(Category category)
    {
        using (var context = new AppDbContext())
        {
            var existingCategory = await context.Categories.FindAsync(category.Id);
            if (existingCategory != null)
            {
                context.Entry(existingCategory).CurrentValues.SetValues(category);
                await context.SaveChangesAsync();
            }
        }
    }

    public static async Task DeleteCategoryAsync(int categoryId)
    {
        using (var context = new AppDbContext())
        {
            var category = await context.Categories.FindAsync(categoryId);
            if (category != null)
            {
                context.Categories.Remove(category);
                await context.SaveChangesAsync();
            }
        }
    }
}
