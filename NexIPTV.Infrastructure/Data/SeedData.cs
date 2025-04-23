using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexIPTV.Infrastructure.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using var context = new AppDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>());

            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Create Admin Role
            if (!await roleManager.RoleExistsAsync("Admin"))
                await roleManager.CreateAsync(new IdentityRole("Admin"));

            // Create Admin User
            if (await userManager.FindByEmailAsync("admin@nexiptv.com") == null)
            {
                var admin = new ApplicationUser
                {
                    UserName = "admin@nexiptv.com",
                    Email = "admin@nexiptv.com",
                    CreditBalance = decimal.MaxValue,
                    IsTrial = false,
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(admin, "SecurePassword123!");
                await userManager.AddToRoleAsync(admin, "Admin");
            }
        }
    }
}
