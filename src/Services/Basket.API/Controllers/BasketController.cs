using Basket.API.gRPCServices;
using Basket.API.Interfaces;
using Basket.API.Properties.Models;
using Discount.gRPC.Protos;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BasketController : ControllerBase
{
    private readonly IBasketRepository _basketRepository;
    private readonly DiscountgRpcService _discountgRpcService;

    public BasketController(IBasketRepository basketRepository, DiscountgRpcService discountgRpcService)
    {
        _basketRepository = basketRepository;
        _discountgRpcService = discountgRpcService;
    }

    [HttpGet]
    public async Task<IActionResult> GetBasket(string userName)
    {
        return Ok(await _basketRepository.GetAsync(userName));
    }

    [HttpPost]
    public async Task<IActionResult> Update([FromBody] ShoppingCart shopppingCart)
    {
        foreach (var item in shopppingCart.Items)
        {
            var coupon = await _discountgRpcService.GetDiscount(item.ProductId);
            item.Price -= coupon.Amount;
        }
        return Ok(await _basketRepository.UpdateAsync(shopppingCart));
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(string userName)
    {
        await _basketRepository.DeleteAsync(userName);
        return NoContent();
    }
}