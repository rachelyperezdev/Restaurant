using Microsoft.AspNetCore.Identity;
using Restaurant.Core.Application.Enums;
using Restaurant.Infrastructure.Identity.Entities;

namespace Restaurant.Infrastructure.Identity.Seeds
{
    public static class DefaultAdmin
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            ApplicationUser defaultAdmin = new();
            defaultAdmin.UserName = "basicAdmin";
            defaultAdmin.Email = "basicAdmin@gmail.com";
            defaultAdmin.FirstName = "John";
            defaultAdmin.LastName = "Smith";
            defaultAdmin.EmailConfirmed = true;
            defaultAdmin.PhoneNumberConfirmed = true;

            if (userManager.Users.All(u => u.Id != defaultAdmin.Id))
            {
                var user = await userManager.FindByNameAsync(defaultAdmin.UserName);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultAdmin, "456Pa$$word!");
                    await userManager.AddToRoleAsync(defaultAdmin, Roles.Admin.ToString());
                }
            }

        }
    }
}
