using Microsoft.AspNetCore.Identity;
using Restaurant.Core.Application.Enums;
using Restaurant.Infrastructure.Identity.Entities;

namespace Restaurant.Infrastructure.Identity.Seeds
{
    public static class DefaultWaiter
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            ApplicationUser defaultWaiter = new();
            defaultWaiter.UserName = "basicWaiter";
            defaultWaiter.Email = "basicWaiter@email.com";
            defaultWaiter.FirstName = "David";
            defaultWaiter.LastName = "Brown";
            defaultWaiter.EmailConfirmed = true;
            defaultWaiter.PhoneNumberConfirmed = true;

            if (userManager.Users.All(u => u.Id != defaultWaiter.Id))
            {
                var user = await userManager.FindByNameAsync(defaultWaiter.UserName);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultWaiter, "987Pa$$word!");
                    await userManager.AddToRoleAsync(defaultWaiter, Roles.Waiter.ToString());
                }
            }
        }
    }
}
