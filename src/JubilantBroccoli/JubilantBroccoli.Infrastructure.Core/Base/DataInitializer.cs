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
            var roles = AppData.Roles.ToArray();
            //var roleStore = new RoleStore<IdentityRole>(context);
            //foreach (var role in roles)
            //{
            //    if (!roleStore.Roles.Any(x => x.Name == role))
            //    {
            //        await roleStore.CreateAsync(new IdentityRole(role)
            //        {
            //            NormalizedName = role.ToUpper()
            //        });
            //    }
            //}

            const string username = "shahoval99@mail.ru";

            if (context.Users.Any(x => x.Email == username))
            {
                return;
            }

            var user = new IdentityUser
            {
                Email = username,
                EmailConfirmed = true,
                NormalizedEmail = username.ToUpper(),
                PhoneNumber = "+790000000000",
                UserName = username,
                PhoneNumberConfirmed = true,
                NormalizedUserName = username.ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString("D")
            };

            var passwordHasher = new PasswordHasher<IdentityUser>();
            user.PasswordHash = passwordHasher.HashPassword(user, "123qwe!@#");

            var userStore = new UserStore<IdentityUser>(context);
            var identityResult = await userStore.CreateAsync(user);
            if (!identityResult.Succeeded)
            {
                var message = string.Join(", ", identityResult.Errors.Select(x => $"{x.Code}: {x.Description}"));
                throw new Exception(message);
            }

            var userManager = scope.ServiceProvider.GetService<UserManager<IdentityUser>>();
            foreach (var role in roles)
            {
                var identityResultRole = await userManager!.AddToRoleAsync(user, role);
                if (!identityResultRole.Succeeded)
                {
                    var message = string.Join(", ", identityResultRole.Errors.Select(x => $"{x.Code}: {x.Description}"));
                    throw new Exception(message);
                }

            }

            await context.SaveChangesAsync();
        }
    }
}