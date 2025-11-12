using BussinessObject.Entities;
using DataAccess.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories;

public class ProductRepository : IProductRepository
{
    public Task<List<Product>> GetProductsAsync() => ProductDAO.GetProductsAsync();
    public Task<Product?> GetProductByIdAsync(int productId) => ProductDAO.GetProductByIdAsync(productId);
    public Task AddProductAsync(Product product) => ProductDAO.AddProductAsync(product);
    public Task UpdateProductAsync(Product product) => ProductDAO.UpdateProductAsync(product);
    public Task DeleteProductAsync(int productId) => ProductDAO.DeleteProductAsync(productId);
}
