using BussinessObject.Entities;
using DataAccess.DAO;

namespace DataAccess.Repositories;

public class CateogoryRepository : ICategoryRepository
{
    public Task<List<Category>> GetCategoriesAsync() => CategoryDAO.GetcategoriesAsync();
    public Task<Category?> GetCategoryByIdAsync(int categoryId) => CategoryDAO.GetCategoryByIdAsync(categoryId);
    public Task AddCategoryAsync(Category category) => CategoryDAO.AddCategoryAsync(category);
    public Task UpdateCategoryAsync(Category category) => CategoryDAO.UpdateCategoryAsync(category);
    public Task DeleteCategoryAsync(int categoryId) => CategoryDAO.DeleteCategoryAsync(categoryId);
}
