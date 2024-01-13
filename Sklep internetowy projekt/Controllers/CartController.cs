using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Sklep_internetowy_projekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;

public class CartController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly ICartService _cartService;

    public CartController(ApplicationDbContext context, ICartService cartService)
    {
        _context = context;
        _cartService = cartService;
    }

    public IActionResult Index()
    {
        ShoppingCart cart = GetShoppingCart();
        return View(cart.Items);
    }

    public IActionResult AddToCart(int productId, int quantity)
    {
        var product = _context.Products.Find(productId);
        if (product == null)
        {
            return NotFound();
        }

        AddItemToCart(product, quantity);

        // Redirect to the cart page
        return RedirectToAction("Index");
    }

    private ShoppingCart GetShoppingCart()
    {
        string cartJson = HttpContext.Session.GetString("Cart");
        ShoppingCart cart = string.IsNullOrEmpty(cartJson) ? new ShoppingCart() : JsonConvert.DeserializeObject<ShoppingCart>(cartJson);
        return cart;
    }

    private void AddItemToCart(Product product, int quantity)
    {
        ShoppingCart cart = GetShoppingCart();

        var existingItem = cart.Items.FirstOrDefault(item => item.ProductId == product.ProductId);

        if (existingItem != null)
        {
            existingItem.Quantity++; // Increment the quantity if the item already exists
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
            cart.Items.Add(newItem); // Add a new item to the cart
        }
        string cartJson = JsonConvert.SerializeObject(cart);
        HttpContext.Session.SetString("Cart", cartJson);
    }
}