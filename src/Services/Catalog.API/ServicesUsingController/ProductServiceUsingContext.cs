using Catalog.API.Data;
using Catalog.API.Interfaces;
using Catalog.API.Models;
using MongoDB.Driver;

namespace Catalog.API.ServicesUsingController;

public class ProductServiceUsingContext : IProductService
{
    private readonly IMongoCollection<Product> _product;

    public ProductServiceUsingContext(CatalogMongoDbContext context)
    {
        _product = context.Products;
    }

    public async Task<List<Product>> GetAllAsync()
    {
        return await _product.Find(_ => true).ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(string id)
    {
        return await _product.Find(p => p.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Product?> CreateAsync(Product product)
    {
        await _product.InsertOneAsync(product);
        return product;
    }

    public async Task UpdateAsync(Product product)
    {
        await _product.ReplaceOneAsync(p => p.Id == product.Id, product);
    }

    public async Task DeleteAsync(string id)
    {
        await _product.DeleteOneAsync(p => p.Id == id);
    }
}