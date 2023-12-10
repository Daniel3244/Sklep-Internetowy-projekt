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
}
