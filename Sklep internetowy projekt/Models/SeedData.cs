using Microsoft.AspNetCore.Identity;
using Sklep_internetowy_projekt.Models;

public static class SeedData
{
    public static async Task Initialize(IServiceProvider serviceProvider, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        string[] roleNames = { "Admin", "User" };
        IdentityResult roleResult;

        foreach (var roleName in roleNames)
        {
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                // Create the roles and seed them to the database
                roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        ApplicationUser user = await userManager.FindByEmailAsync("admin@admin.com");

        if (user == null)
        {
            user = new ApplicationUser()
            {
                UserName = "admin",
                Email = "admin@admin.com",
            };
            await userManager.CreateAsync(user, "Admin@123");
        }
        await userManager.AddToRoleAsync(user, "Admin");
    }
}
