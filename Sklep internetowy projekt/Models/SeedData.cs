using Microsoft.AspNetCore.Identity;
using Sklep_internetowy_projekt.Models;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Data.Entity;
using Microsoft.EntityFrameworkCore;

public class SeedData
{
    public static async Task Initialize(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
    {
        string roleName = "Admin";
        IdentityResult roleResult;

        var roleExist = await roleManager.RoleExistsAsync(roleName);

        if (!roleExist)
        {
            roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
        }

        var admin = new ApplicationUser
        {
            UserName = "admin@admin.com",
            Email = "admin@admin.com",
            FirstName = "Admin",
            LastName = "Adminn"
        };

        string adminPassword = "@@r_D*ZMQ7wWYeZ";

        var user = await userManager.FindByEmailAsync(admin.Email);

        if (user == null)
        {
            var createAdmin = await userManager.CreateAsync(admin, adminPassword);

            if (createAdmin.Succeeded)
            {
                await userManager.AddToRoleAsync(admin, roleName);
                Console.WriteLine("Admin created");
            }
        }
        var productCount = context.Products.Count();
        if (productCount < 1)
        {
            var products = new List<Product>
            {
                    new Product
                    {
                        Name = "Laptop Gamingowy XYZ",
                        Desc = "Wysokowydajny laptop do gier z procesorem Intel i9, 16GB RAM, 1TB SSD, i kartą graficzną NVIDIA RTX 3070.",
                        Price = 4999.99m,
                        ImagePath = "laptop_gamingowy.jpg"
                    },
                    new Product
                    {
                       Name = "Mysz Gamingowa Alpha",
                       Desc = "Ergonomiczna mysz dla graczy z regulowaną DPI do 12000, podświetleniem RGB i 8 programowalnymi przyciskami.",
                       Price = 299.99m,
                       ImagePath = "mysz_gamingowa.jpg"
                    },
                    new Product
                    {
                       Name = "Klawiatura Mechaniczna Pro",
                       Desc = "Klawiatura mechaniczna z przełącznikami Cherry MX Blue, podświetleniem LED i odporną na zużycie konstrukcją.",
                       Price = 399.99m,
                       ImagePath = "klawiatura_mechaniczna.jpg"
                    },
                    new Product
                    {
                       Name = "Monitor 4K UltraSharp",
                       Desc = "27-calowy monitor 4K UHD, z technologią IPS, czasem reakcji 1 ms i wsparciem dla HDR10.",
                       Price = 2299.99m,
                       ImagePath = "monitor_4k_ultrasharp.jpg"
                    },
                    new Product
                    {
                        Name = "Dysk SSD SuperFast 1TB",
                        Desc = "Szybki dysk SSD o pojemności 1TB, idealny do upgrade'u komputera lub laptopa.",
                        Price = 599.99m,
                        ImagePath = "dysk_ssd.jpg"
                    } 
            };
            foreach (var product in products)
            {
                context.Products.Add(product);
            }

            await context.SaveChangesAsync();
        }
    }
}

