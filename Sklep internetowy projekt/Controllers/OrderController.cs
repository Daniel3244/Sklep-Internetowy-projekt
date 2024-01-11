using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Sklep_internetowy_projekt.Models;
using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

public class OrderController : Controller
{
    private readonly ApplicationDbContext _context;

    public OrderController(ApplicationDbContext context)
    {
        _context = context;
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
        var order = new Order();
        return View(order);
    }

    [HttpPost]
    public IActionResult PlaceOrder(Order order)
    {
        if (!ModelState.IsValid)
        {
            order.OrderDate = DateTime.Now;

            // Przetwarzaj wybrane produkty bezpośrednio z obiektu order
            foreach (var item in order.SelectedProducts)
            {
                var product = _context.Products.Find(item.ProductId);
                if (product != null)
                {
                    order.OrderProducts.Add(new OrderProduct
                    {
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

        // Jeśli ModelState jest nieprawidłowy, zwracamy widok z modelem "order"
        return View(order);
    }


    public IActionResult OrderConfirmation(int orderId)
    {
        var order = _context.Orders.FirstOrDefault(o => o.OrderId == orderId);

        if (order == null)
        {
            // Obsługa sytuacji, gdy nie znaleziono zamówienia
            return NotFound();
        }

        // Sprawdź, czy istnieją produkty w zamówieniu
        if (order.OrderProducts == null || !order.OrderProducts.Any())
        {
            // Obsługa sytuacji, gdy zamówienie nie zawiera produktów
            return NotFound();
        }

        decimal totalPrice = CalculateTotalPrice(order);
        ViewBag.TotalPrice = totalPrice;

        return View(order);
    }

    private decimal CalculateTotalPrice(Order order)
    {
        decimal totalPrice = 0;

        foreach (var orderProduct in order.OrderProducts)
        {
            totalPrice += orderProduct.Quantity * orderProduct.UnitPrice;
        }

        return totalPrice;
    }

}
