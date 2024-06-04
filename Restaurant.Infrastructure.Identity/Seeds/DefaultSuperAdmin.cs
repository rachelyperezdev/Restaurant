using Microsoft.AspNetCore.Identity;
using Restaurant.Core.Application.Enums;
using Restaurant.Infrastructure.Identity.Entities;

namespace Restaurant.Infrastructure.Identity.Seeds
{
    public static class DefaultSuperAdmin
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            ApplicationUser defaultSuperAdmin = new();
            defaultSuperAdmin.UserName = "superAdminUser";
            defaultSuperAdmin.Email = "superadminuser@gmail.com";
            defaultSuperAdmin.FirstName = "Tom";
            defaultSuperAdmin.LastName = "Davis";
            defaultSuperAdmin.EmailConfirmed = true;
            defaultSuperAdmin.PhoneNumberConfirmed = true;

            if (userManager.Users.All(u => u.Id != defaultSuperAdmin.Id))
            {
                var user = await userManager.FindByNameAsync(defaultSuperAdmin.UserName);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultSuperAdmin, "123Pa$$word!");
                    await userManager.AddToRoleAsync(defaultSuperAdmin, Roles.Waiter.ToString());
                    await userManager.AddToRoleAsync(defaultSuperAdmin, Roles.Admin.ToString());
                    await userManager.AddToRoleAsync(defaultSuperAdmin, Roles.SuperAdmin.ToString());

                }
            }
        }
    }
}
