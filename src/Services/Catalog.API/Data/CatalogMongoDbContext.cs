using Catalog.API.Dtos;
using Catalog.API.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Catalog.API.Data;

public class CatalogMongoDbContext
{
    private readonly IMongoDatabase _database;
    private readonly MongoSettings _settings;

    public CatalogMongoDbContext(IOptionsSnapshot<MongoSettings> settings)
    {
        _settings = settings.Value;
        var client = new MongoClient(_settings.ConnectionString);
        _database = client.GetDatabase(_settings.DatabaseName);
    }

    public IMongoCollection<Product> Products => _database.GetCollection<Product>("Products");
}