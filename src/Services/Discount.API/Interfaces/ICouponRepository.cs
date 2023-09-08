using Discount.API.Models;

namespace Discount.API.Interfaces;

public interface ICouponRepository
{
    Task<Coupon> GetDiscount(string productId);
    Task<Coupon?> CreateDiscount(Coupon coupon);
    Task<bool> UpdateDiscount(Coupon coupon);
    Task<bool> DeleteDiscount(string productId);
}
