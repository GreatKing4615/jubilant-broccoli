using JubilantBroccoli.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace JubilantBroccoli.Infrastructure.Core.Base
{
    public static class DataInitializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {

            var scope = serviceProvider.CreateScope();
            await using var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            await context.Database.MigrateAsync();
            await RegisteredUsers(context, scope);


            await context.SaveChangesAsync();
        }

        public static async Task RegisteredUsers(ApplicationDbContext context, IServiceScope scope)
        {
            const string email = "123123123@mail.ru";
            const string password = "123123123";
            const string phone = "+790000000000";
            const string UserName = "PikaPi";
            
            var userManager = scope.ServiceProvider.GetService<UserManager<IdentityUser>>();
            var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
            if (userManager == null || roleManager == null)
            {
                throw new ArgumentNullException("UserManager or RoleManager not registered");
            }
            var roles = AppData.Roles.ToArray();

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }

            if (await userManager.FindByEmailAsync(email) is not null)
            {
                return;
            }

            var user = new IdentityUser
            {
                Email = email,
                EmailConfirmed = true,
                NormalizedEmail = email.ToUpper(),
                PhoneNumber = phone,
                UserName = UserName,
                PhoneNumberConfirmed = true,
                NormalizedUserName = UserName.ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString("D")
            };

            var passwordHasher = new PasswordHasher<IdentityUser>();
            user.PasswordHash = passwordHasher.HashPassword(user, password);

            var userStore = new UserStore<IdentityUser>(context);
            var identityResult = await userStore.CreateAsync(user);
            if (!identityResult.Succeeded)
            {
                var message = string.Join(", ", identityResult.Errors.Select(x => $"{x.Code}: {x.Description}"));
                throw new Exception(message);
            }

            foreach (var role in roles)
            {
                var identityResultRole = await userManager!.AddToRoleAsync(user, role);
                if (!identityResultRole.Succeeded)
                {
                    var message = string.Join(", ", identityResultRole.Errors.Select(x => $"{x.Code}: {x.Description}"));
                    throw new Exception(message);
                }

            }
        }
    }
}