using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace SimpleStore.DataAccess.Seeds
{
    public static class UserSeed
    {
        public static async Task Seed(IServiceProvider service)
        {
            // UserManager (Repositorio)
            var userManager = service.GetRequiredService<UserManager<SimpleStoreUserIdentity>>();

            // RoleManager (Repositorio)
            var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();

            // Crear Roles
            var adminRole = new IdentityRole("Admin");
            var userRole = new IdentityRole("User");

            if (!await roleManager.RoleExistsAsync("Admin"))
                await roleManager.CreateAsync(adminRole);

            if (!await roleManager.RoleExistsAsync("User"))
                await roleManager.CreateAsync(userRole);

            var adminUser = new SimpleStoreUserIdentity
            {
                FirstName = "Administrador",
                LastName = "",
                Email = "admin@gmail.com",
                UserName = "admin@gmail.com",
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(adminUser, "P1$$w0rd");
            if (result.Succeeded)
            {
                adminUser = await userManager.FindByEmailAsync(adminUser.Email);
                if (adminUser is not null)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}
