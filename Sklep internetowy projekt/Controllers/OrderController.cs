using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Sklep_internetowy_projekt.Models;
using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Cors.Infrastructure;

public class OrderController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly ICartService _cartService;


    public OrderController(ICartService cartService, ApplicationDbContext context)
    {
        _context = context;
        _cartService = cartService;
    }

    public IActionResult ManageOrders()
    {
        var orders = _context.Orders.ToList();
        return View(orders);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Order order)
    {
        if (ModelState.IsValid)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
            return RedirectToAction("ManageOrders");
        }
        return View(order);
    }

    [HttpGet]
    public IActionResult Checkout()
    {
        var order = _cartService.PrepareOrderForCheckout();

        // Ensure that SelectedProducts is not null or empty
        if (order.SelectedProducts == null || !order.SelectedProducts.Any())
        {
            // Handle the case where no products are selected, maybe redirect to cart page
            return RedirectToAction("Index", "Cart");
        }

        return View(order);
    }

    [HttpPost]
    public IActionResult PlaceOrder(Order order)
    {
        if (ModelState.IsValid)
        {
            return View("Checkout", order);
        }

        // Retrieve the shopping cart again, as SelectedProducts might not be coming through
        var cart = _cartService.GetShoppingCart();
        order.SelectedProducts = cart.Items;

        // Check if cart items exist
        if (order.SelectedProducts == null || !order.SelectedProducts.Any())
        {
            // Handle the case where no products are selected
            return View("Error"); // or any appropriate response
        }

        order.OrderDate = DateTime.Now;
        order.OrderProducts = new List<OrderProduct>();

        foreach (var item in order.SelectedProducts)
        {
            var product = _context.Products.Find(item.ProductId);
            if (product != null)
            {
                order.OrderProducts.Add(new OrderProduct
                {
                    // Don't set an ID here if it's an identity column
                    Product = product,
                    Quantity = item.Quantity,
                    UnitPrice = product.Price
                });
            }
        }

        _context.Orders.Add(order);
        _context.SaveChanges();

        return RedirectToAction("OrderConfirmation", new { orderId = order.OrderId });
    }
    public IActionResult OrderConfirmation(int orderId)
    {
        var order = _context.Orders.FirstOrDefault(o => o.OrderId == orderId);
        return RedirectToAction("Index", "Product");
        

    }


}
