using Basket.API.Properties.Models;

namespace Basket.API.Interfaces;

public interface IBasketRepository
{
    Task<ShoppingCart> GetAsync(string userName);
    Task<ShoppingCart> UpdateAsync(ShoppingCart basket);
    Task DeleteAsync(string userName);
}