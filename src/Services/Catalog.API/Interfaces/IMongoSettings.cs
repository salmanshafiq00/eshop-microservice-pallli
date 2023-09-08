namespace Catalog.API.Interfaces;

public interface IMongoSettings
{
    string ConnectionString { get; set; }
    string DatabaseName { get; set; }
    string ProductCollectionName { get; set; }
}