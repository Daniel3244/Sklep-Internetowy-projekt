using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Sklep_internetowy_projekt.Models;

public class CartController : Controller
{
    private readonly ApplicationDbContext _context;

    public CartController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        ShoppingCart cart = GetShoppingCart();
        return View(cart.Items);
    }

    public IActionResult AddToCart(int productId)
    {
        var product = _context.Products.Find(productId);
        AddItemToCart(product);

        return RedirectToAction("Index");
    }

    private ShoppingCart GetShoppingCart()
    {
        string cartJson = HttpContext.Session.GetString("Cart");
        ShoppingCart cart = string.IsNullOrEmpty(cartJson) ? new ShoppingCart() : JsonConvert.DeserializeObject<ShoppingCart>(cartJson);
        return cart;
    }

    private void AddItemToCart(Product product)
    {
        ShoppingCart cart = GetShoppingCart();

        var existingItem = cart.Items.FirstOrDefault(item => item.ProductId == product.ProductId);

        if (existingItem != null)
        {
            existingItem.Quantity++;
        }
        else
        {
            var newItem = new ShoppingCartItem
            {
                ProductId = product.ProductId,
                ProductName = product.Name,
                Price = product.Price,
                Quantity = 1
            };
            cart.Items.Add(newItem);
        }

        // Pobierz zamówienie z sesji lub utwórz nowe, jeśli nie istnieje
        var orderJson = HttpContext.Session.GetString("Order");
        Order order = string.IsNullOrEmpty(orderJson) ? new Order() : JsonConvert.DeserializeObject<Order>(orderJson);

        // Upewnij się, że SelectedProducts są zainicjowane
        if (order.SelectedProducts == null)
        {
            order.SelectedProducts = new List<ShoppingCartItem>();
        }

        // Dodaj produkt do listy wybranych produktów w zamówieniu
        order.SelectedProducts.Add(new ShoppingCartItem
        {
            ProductId = product.ProductId,
            ProductName = product.Name,
            Price = product.Price,
            Quantity = 1
        });

        // Zapisz zamówienie z powrotem do sesji
        orderJson = JsonConvert.SerializeObject(order);
        HttpContext.Session.SetString("Order", orderJson);

        string cartJson = JsonConvert.SerializeObject(cart);
        HttpContext.Session.SetString("Cart", cartJson);
    }




}
