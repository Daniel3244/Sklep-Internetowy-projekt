using Newtonsoft.Json;
using Sklep_internetowy_projekt.Models;

public interface ICartService
{
    Order PrepareOrderForCheckout();
    ShoppingCart GetShoppingCart();
}

public class CartService : ICartService
{
    private readonly ApplicationDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CartService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public Order PrepareOrderForCheckout()
    {
        string cartJson = _httpContextAccessor.HttpContext.Session.GetString("Cart");
        ShoppingCart cart = string.IsNullOrEmpty(cartJson) ? new ShoppingCart() : JsonConvert.DeserializeObject<ShoppingCart>(cartJson);

        var order = new Order
        {
            SelectedProducts = cart.Items,
            OrderDate = DateTime.Now
        };

        return order;
    }

    public ShoppingCart GetShoppingCart()
    {
        string cartJson = _httpContextAccessor.HttpContext.Session.GetString("Cart");
        return string.IsNullOrEmpty(cartJson) ? new ShoppingCart() : JsonConvert.DeserializeObject<ShoppingCart>(cartJson);
    }
}


