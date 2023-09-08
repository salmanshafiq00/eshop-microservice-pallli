using System.Data;
using Npgsql;

namespace Discount.API.Data;

public class DapperContext 
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;
    public DapperContext(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("DiscountDbConnection");
    }

    public IDbConnection CreateConnection() => new NpgsqlConnection(_connectionString);
}
