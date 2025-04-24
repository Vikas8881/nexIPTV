using Microsoft.AspNetCore.Identity;
using NexIPTV.API.Entities;
using NexIPTV.Data;

namespace NexIPTV.API.Data
{
    public static class SeedData
    {
        public static async Task Initialize(
            AppDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            // Create roles
            if (!await roleManager.RoleExistsAsync("Admin"))
                await roleManager.CreateAsync(new IdentityRole("Admin"));

            if (!await roleManager.RoleExistsAsync("Reseller"))
                await roleManager.CreateAsync(new IdentityRole("Reseller"));

            if (!await roleManager.RoleExistsAsync("User"))
                await roleManager.CreateAsync(new IdentityRole("User"));

            // Create admin user
            var adminEmail = "admin@nexiptv.com";
            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var admin = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    CreditBalance = decimal.MaxValue,
                    IsTrial = false,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(admin, "Admin@1234!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }
        }
    }
}