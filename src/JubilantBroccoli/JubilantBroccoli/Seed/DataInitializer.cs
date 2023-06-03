using JubilantBroccoli.BusinessLogic.Implementations;
using JubilantBroccoli.Domain.Models;
using JubilantBroccoli.Infrastructure.Core.Base;
using JubilantBroccoli.Infrastructure.UnitOfWork.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace JubilantBroccoli.Seed
{
    public static class DataInitializer
    {
        public static IUnitOfWork _unitOfWork;
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var scope = serviceProvider.CreateScope();
            await using var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            _unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            var itemRepository = _unitOfWork.GetRepository<Item>();
            await context.Database.MigrateAsync();
            await SeedUsers(context, scope);
            await SeedMainData(itemRepository);
            await _unitOfWork.SaveChangesAsync();
        }

        private static async Task SeedUsers(ApplicationDbContext context, IServiceScope scope)
        {
            var userManager = scope.ServiceProvider.GetService<UserManager<IdentityUser>>();
            var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
            if (userManager == null || roleManager == null)
            {
                throw new ArgumentNullException("UserManager or RoleManager not registered");
            }
            var roles = AppData.Roles.ToArray();

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            var users = UserHelper.GetUsers();
            var passwordHasher = new PasswordHasher<User>();

            foreach (var user in users)
            {
                var existedUser = await userManager.FindByEmailAsync(user.Email);
                var password = passwordHasher.HashPassword(user, UserHelper.GetPasswordsByEmail(user.Email));
                if (existedUser is null)
                {
                    IdentityResultHandler(
                        await userManager.CreateAsync(user, password)
                    );
                    IdentityResultHandler(
                        await userManager.AddToRoleAsync(user, user.Role)
                    );
                    await context.SaveChangesAsync();
                }
            }
        }

        private static async Task SeedMainData(IRepository<Item> itemRepository)
        {
            if (!await itemRepository.ExistsAsync())
            {
                var defaultItems = ItemHelper.GetItems();
                var itemOptions = ItemOptionsHelper.GetItemOptions();
                var defaultRestaurants = RestaurantHelper.GetRestaurants();
                foreach (var item in defaultItems)
                {
                    var randomItemOptions = itemOptions.Where(x => Equals(item.Type, x.Type))
                        .OrderBy(_ => Randomiser.GetRandomNumber())
                        .Distinct()
                        .Take(5)
                        .ToList();

                    item.ItemOptions = randomItemOptions;
                    var targetRestaurants = defaultRestaurants.Where(x => x.ItemTypes.Contains(item.Type)).ToList();
                    item.Restaurants = targetRestaurants;
                }

                await itemRepository.InsertAsync(defaultItems, CancellationToken.None);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        private static void IdentityResultHandler(IdentityResult result)
        {
            if (result.Succeeded is false)
            {
                var message = string.Join(", ", result.Errors.Select(x => $"{x.Code}: {x.Description}"));
                throw new Exception(message);
            }
        }
    }
}