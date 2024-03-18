using Microsoft.AspNetCore.Identity;
using CSSC.Areas.Identity.Data;

namespace CSSC
{
    static class Configurations
    {
        public static async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<CSSCUser>>();
            string[] roleNames = { "Operador", "Default" };
            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist) await roleManager.CreateAsync(new IdentityRole(roleName));
            }
            var manager = new CSSCUser
            {
                UtDataDeNascimento = "18/05/1992",
                UtMorada = "Rua Teste",
                UtNIF = 123456789,
                UserName = "cssc.esa@gmail.com",
                Email = "cssc.esa@gmail.com"
            };
            var _user = await userManager.FindByEmailAsync(manager.Email);
            var code = await userManager.GenerateEmailConfirmationTokenAsync(manager);
            await userManager.ConfirmEmailAsync(manager, code);
            if (_user != null) return;
            var createPowerUser = await userManager.CreateAsync(manager, "Password_123");
            if (createPowerUser.Succeeded)
                await userManager.AddToRoleAsync(manager, "Operador");
        }
    }
}
