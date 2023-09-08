using Catalog.API.Models;

namespace Catalog.API.Interfaces;

public interface IProductService
{
    Task<List<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(string id);
    Task<Product?> CreateAsync(Product product);
    Task UpdateAsync(Product product);
    Task DeleteAsync(string id);
}