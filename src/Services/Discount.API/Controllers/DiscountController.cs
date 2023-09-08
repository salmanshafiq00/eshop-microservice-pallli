using Discount.API.Interfaces;
using Discount.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Discount.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class DiscountController : ControllerBase
{
    private readonly ICouponRepository _couponRepository;
    public DiscountController(ICouponRepository couponRepository)
    {
        _couponRepository = couponRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetDiscount(string productId)
    {
        return Ok(await _couponRepository.GetDiscount(productId));
    }

    [HttpPost]
    public async Task<IActionResult> CreateDiscount([FromBody] Coupon coupon)
    {
        var newCoupon = await _couponRepository.CreateDiscount(coupon);
        if (newCoupon is null)
            return NotFound();
        return CreatedAtAction(nameof(GetDiscount), new {productId = coupon.ProductId}, newCoupon);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateDiscount([FromBody] Coupon coupon)
    {
        var existedCoupon = await _couponRepository.GetDiscount(coupon.ProductId);
        if (existedCoupon is null)
            return NotFound();
        await _couponRepository.UpdateDiscount(coupon);
        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteDiscount(string productId)
    {
        var coupon = await _couponRepository.GetDiscount(productId);
        if (coupon is null)
            return NotFound();
        await _couponRepository.DeleteDiscount(productId);
        return NoContent();
    }
}
