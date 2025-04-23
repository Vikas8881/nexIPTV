using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexIPTV.Infrastructure.Data
{
    public static class SeedData
    {
        public static async Task SeedAdminAsync(UserManager<User> userManager)
        {
            var admin = new User
            {
                UserName = "admin@nexiptv.com",
                Email = "admin@nexiptv.com",
                CreditBalance = decimal.MaxValue,
                IsTrial = false,
                EmailConfirmed = true
            };

            await userManager.CreateAsync(admin, "Admin@1234!");
            await userManager.AddToRoleAsync(admin, "Admin");
        }
    }
}
