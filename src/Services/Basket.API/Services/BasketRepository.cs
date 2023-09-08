using Basket.API.Interfaces;
using Basket.API.Properties.Models;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.API.Services;

public class BasketRepository : IBasketRepository
{
    private readonly IDistributedCache _redisCache;

    public BasketRepository(IDistributedCache redisCache)
    {
        _redisCache = redisCache;
    }

    public async Task<ShoppingCart> GetAsync(string userName)
    {
        var basket = await _redisCache.GetStringAsync(userName);
        return string.IsNullOrEmpty(basket) ? null : JsonConvert.DeserializeObject<ShoppingCart>(basket);
    }

    public async Task<ShoppingCart> UpdateAsync(ShoppingCart basket)
    {
        await _redisCache.SetStringAsync(basket.UserName, JsonConvert.SerializeObject(basket));
        return await GetAsync(basket.UserName);
    }

    public async Task DeleteAsync(string userName)
    {
        await _redisCache.RemoveAsync(userName);
    }
}