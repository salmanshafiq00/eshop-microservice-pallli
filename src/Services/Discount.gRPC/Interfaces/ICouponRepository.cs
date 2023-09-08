using Discount.gRPC.Models;

namespace Discount.gRPC.Interfaces;

public interface ICouponRepository
{
    Task<Coupon> GetDiscount(string productId);
    Task<Coupon?> CreateDiscount(Coupon coupon);
    Task<bool> UpdateDiscount(Coupon coupon);
    Task<bool> DeleteDiscount(string productId);
}