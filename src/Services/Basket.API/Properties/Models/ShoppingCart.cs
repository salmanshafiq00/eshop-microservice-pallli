namespace Basket.API.Properties.Models;

public class ShoppingCart
{
    public string UserName { get; set; }
    public List<ShoppingCartItem> Items { get; set; } = new();


    public decimal TotalPrice
    {
        get
        {
            decimal totalPrice = 0;
            foreach (var item in Items) totalPrice += item.Price;
            return totalPrice;
        }
    }
}