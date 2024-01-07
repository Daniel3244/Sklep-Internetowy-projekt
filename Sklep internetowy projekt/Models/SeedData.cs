using Microsoft.AspNetCore.Identity;
using Sklep_internetowy_projekt.Models;
using System;
using System.Threading.Tasks;

public class SeedData
{
    public static async Task Initialize(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        string roleName = "Admin";
        IdentityResult roleResult;

        var roleExist = await roleManager.RoleExistsAsync(roleName);

        // Create the Admin role if it doesn't exist
        if (!roleExist)
        {
            roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
        }

        // Create the Admin user
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
                // Add the Admin user to the Admin role
                await userManager.AddToRoleAsync(admin, roleName);
                Console.WriteLine("Admin created");
            }
        }
    }
}
