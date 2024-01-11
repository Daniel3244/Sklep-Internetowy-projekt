using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Sklep_internetowy_projekt.Models;
using System;
using System.Linq;

public class OrderController : Controller
{
    private readonly ApplicationDbContext _context;

    public OrderController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult ManageOrders()
    {
        var orders = _context.Order.ToList();
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
            _context.Order.Add(order);
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
            // Save order details to the database
            _context.Order.Add(order);
           
           _context.SaveChanges();

            // You may want to clear the shopping cart or perform other actions

            return RedirectToAction("OrderConfirmation", new { orderId = order.OrderId });
        }

        // If ModelState is not valid, return to the checkout page with errors
        return View("Checkout", order);
    }
}
