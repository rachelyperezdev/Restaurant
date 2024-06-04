using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.Infrastructure.Identity.Entities;
using Restaurant.Infrastructure.Identity.Seeds;

namespace Restaurant.Infrastructure.Identity
{
    public static class ServiceApplication
    {
        public static async Task AddIdentitySeeds(this IServiceProvider services)
        {
            #region "Identity Seeds"
            using (var scope = services.CreateScope())
            {
                var serviceScope = scope.ServiceProvider;

                try
                {
                    var userManager = serviceScope.GetRequiredService<UserManager<ApplicationUser>>();
                    var roleManager = serviceScope.GetRequiredService<RoleManager<IdentityRole>>();

                    await DefaultRoles.SeedAsync(userManager, roleManager);
                    await DefaultSuperAdmin.SeedAsync(userManager, roleManager);
                    await DefaultAdmin.SeedAsync(userManager, roleManager);
                    await DefaultWaiter.SeedAsync(userManager, roleManager);
                }
                catch (Exception ex)
                {

                }
            }
            #endregion
        }
    }
}
