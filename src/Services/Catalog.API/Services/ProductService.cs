using Catalog.API.Interfaces;
using Catalog.API.Models;
using MongoDB.Driver;

namespace Catalog.API.Services;

public class ProductService : IProductService
{
    private readonly IMongoCollection<Product> _products;

    public ProductService(IMongoSettings settings)
    {
        var client = new MongoClient(settings.ConnectionString);
        var database = client.GetDatabase(settings.DatabaseName);
        _products = database.GetCollection<Product>(settings.ProductCollectionName);
    }

    public async Task<List<Product>> GetAllAsync()
    {
        return await _products.Find(p => true).ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(string id)
    {
        return await _products.Find(p => p.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Product?> CreateAsync(Product product)
    {
        await _products.InsertOneAsync(product);
        return product;
    }

    public async Task UpdateAsync(Product product)
    {
        await _products.ReplaceOneAsync(p => p.Id == product.Id, product);
    }

    public async Task DeleteAsync(string id)
    {
        await _products.DeleteOneAsync(p => p.Id == id);
    }
}