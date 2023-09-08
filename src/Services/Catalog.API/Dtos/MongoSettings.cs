using Catalog.API.Interfaces;

namespace Catalog.API.Dtos;

public class MongoSettings : IMongoSettings
{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string ProductCollectionName { get; set; } = null!;
}