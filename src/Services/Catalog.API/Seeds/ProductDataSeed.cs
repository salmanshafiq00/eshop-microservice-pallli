using Catalog.API.Data;
using Catalog.API.Models;
using MongoDB.Driver;

namespace Catalog.API.Seeds;

public class ProductDataSeed
{
    private readonly IMongoCollection<Product> _productCollection;

    public ProductDataSeed(CatalogMongoDbContext context)
    {
        _productCollection = context.Products;
    }

    public void SeedData()
    {
        // Define the initial data you want to insert
        var initialData = new List<Product>
        {
            new()
            {
                Name = "Apple",
                Description = "World-Class Phone",
                Category = "Smartphone",
                Summary = "World-Class Phone",
                ImageFile = "n/a",
                Price = 120000
            },
            new()
            {
                Name = "Apple2",
                Description = "World-Class Phone 2",
                Category = "Smartphone 2",
                Summary = "World-Class Phone 2",
                ImageFile = "n/a",
                Price = 120000
            }
        };

        // Check if data already exists and skip if it does
        if (_productCollection.CountDocuments(FilterDefinition<Product>.Empty) == 0)
            _productCollection.InsertMany(initialData);
    }
}