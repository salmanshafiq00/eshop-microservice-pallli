using System.Data;
using Dapper;
using Discount.gRPC.Data;
using Discount.gRPC.Interfaces;
using Discount.gRPC.Models;

namespace Discount.gRPC.Repository;

public class CouponRepository : ICouponRepository
{
    private readonly DapperContext _context;

    public CouponRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<Coupon> GetDiscount(string productId)
    {
        var query = "SELECT ProductId, ProductName, Description, Amount " +
                    "FROM Coupons WHERE ProductId = @productId";
        using var connection = _context.CreateConnection();
        var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>(query, new { productId });
        return coupon;
    }

    public async Task<Coupon?> CreateDiscount(Coupon coupon)
    {
        var query = "INSERT INTO Coupons (ProductId, ProductName, Description, Amount) " +
                    "VALUES (@ProductId, @ProductName, @Description, @Amount) RETURNING *";

        var parameters = new DynamicParameters();
        parameters.Add("ProductId", coupon.ProductId, DbType.String);
        parameters.Add("ProductName", coupon.ProductName, DbType.String);
        parameters.Add("Description", coupon.Description, DbType.String);
        parameters.Add("Amount", coupon.Amount, DbType.Int32);

        using var connection = _context.CreateConnection();
        var newCoupon = await connection.QuerySingleOrDefaultAsync<Coupon>(query, parameters);
        return newCoupon;
    }

    public async Task<bool> UpdateDiscount(Coupon coupon)
    {
        var query = "UPDATE Coupons " +
                    "SET ProductId = @ProductId, ProductName = @ProductName, Description = @Description, Amount = @Amount " +
                    "WHERE ProductId = @ProductId";

        var parameters = new DynamicParameters();
        parameters.Add("ProductId", coupon.ProductId, DbType.String);
        parameters.Add("ProductName", coupon.ProductName, DbType.String);
        parameters.Add("Description", coupon.Description, DbType.String);
        parameters.Add("Amount", coupon.Amount, DbType.Int32);

        using var connection = _context.CreateConnection();
        var id = await connection.ExecuteAsync(query, parameters);
        return id > 0;
    }

    public async Task<bool> DeleteDiscount(string productId)
    {
        var query = "DELETE FROM Coupons WHERE ProductId = @ProductId";

        var parameters = new DynamicParameters();
        parameters.Add("ProductId", productId, DbType.String);

        var connection = _context.CreateConnection();
        var id = await connection.ExecuteAsync(query, parameters);

        return id > 0;
    }
}