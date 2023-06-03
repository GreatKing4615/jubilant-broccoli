using JubilantBroccoli.Domain.Core.Enums;
using JubilantBroccoli.Domain.Models;

namespace JubilantBroccoli.Seed;

public static class ItemHelper
{
    public static List<Item> GetItems()
    {
        return new List<Item>{
        //pizza
        new Item
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Маргарита",
            CookingTime = new TimeSpan(hours: 0, minutes: 15, seconds: 0),
            Price = GetRandomPrice(),
            Type = ItemType.Pizza,
            Unit = Unit.piece,
            Weight = 1
        },
        new Item
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Пепперони",
            CookingTime = new TimeSpan(hours: 0, minutes: 18, seconds: 0),
            Price = GetRandomPrice(),
            Type = ItemType.Pizza,
            Unit = Unit.piece,
            Weight = 1
        },
        new Item
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Карбонара",
            CookingTime = new TimeSpan(hours: 0, minutes: 20, seconds: 0),
            Price = GetRandomPrice(),
            Type = ItemType.Pizza,
            Unit = Unit.piece,
            Weight = 1
        },

        //sushi
        new Item
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Калифорния",
            CookingTime = new TimeSpan(hours: 0, minutes: 15, seconds: 0),
            Price = GetRandomPrice(),
            Type = ItemType.Sushi,
            Unit = Unit.piece,
            Weight = 1
        },
        new Item
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Теплый ролл",
            CookingTime = new TimeSpan(hours: 0, minutes: 25, seconds: 0),
            Price = GetRandomPrice(),
            Type = ItemType.Sushi,
            Unit = Unit.piece,
            Weight = 1
        },
        new Item
        {
            Id = Guid.NewGuid().ToString(),
            Name = "ЧикенРолл",
            CookingTime = new TimeSpan(hours: 0, minutes: 20, seconds: 0),
            Price = GetRandomPrice(),
            Type = ItemType.Sushi,
            Unit = Unit.piece,
            Weight = 1
        },
        new Item
        {
            Id = Guid.NewGuid().ToString(),
            Name = "СякиМаки",
            CookingTime = new TimeSpan(hours: 0, minutes: 32, seconds: 00),
            Price = GetRandomPrice(),
            Type = ItemType.Sushi,
            Unit = Unit.piece,
            Weight = 1
        },

        //burgers
        new Item
        {
            Id = Guid.NewGuid().ToString(),
            Name = "БигМак",
            CookingTime = new TimeSpan(hours: 0, minutes: 5, seconds: 30),
            Price = GetRandomPrice(),
            Type = ItemType.Burger,
            Unit = Unit.piece,
            Weight = 1
        },
        new Item
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Чизбургер",
            CookingTime = new TimeSpan(hours: 0, minutes: 7, seconds: 0),
            Price = GetRandomPrice(),
            Type = ItemType.Burger,
            Unit = Unit.piece,
            Weight = 1
        },
        new Item
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Картофель Фри",
            CookingTime = new TimeSpan(hours: 0, minutes: 8, seconds: 0),
            Price = GetRandomPrice(),
            Type = ItemType.Burger,
            Unit = Unit.g,
            Weight = 150
        },

        //beverage
        new Item
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Coca-Cola",
            CookingTime = TimeSpan.Zero,
            Price = GetRandomPrice(),
            Type = ItemType.Beverage,
            Unit = Unit.ml,
            Weight = 330
        },
        new Item
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Sprite",
            CookingTime = TimeSpan.Zero,
            Price = GetRandomPrice(),
            Type = ItemType.Beverage,
            Unit = Unit.ml,
            Weight = 330
        },
        new Item
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Pepsi",
            CookingTime = TimeSpan.Zero,
            Price = GetRandomPrice(),
            Type = ItemType.Beverage,
            Unit = Unit.ml,
            Weight = 330
        },
        new Item
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Fanta",
            CookingTime = TimeSpan.Zero,
            Price = GetRandomPrice(),
            Type = ItemType.Beverage,
            Unit = Unit.ml,
            Weight = 330
        },
        new Item
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Nestea",
            CookingTime = TimeSpan.Zero,
            Price = GetRandomPrice(),
            Type = ItemType.Beverage,
            Unit = Unit.ml,
            Weight = 330
        },
        new Item
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Red Bull",
            CookingTime = TimeSpan.Zero,
            Price = GetRandomPrice(),
            Type = ItemType.Beverage,
            Unit = Unit.ml,
            Weight = 250
        },
        new Item
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Mountain Dew",
            CookingTime = TimeSpan.Zero,
            Price = GetRandomPrice(),
            Type = ItemType.Beverage,
            Unit = Unit.ml,
            Weight = 500
        },
        new Item
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Lipton Ice Tea",
            CookingTime = TimeSpan.Zero,
            Price = GetRandomPrice(),
            Type = ItemType.Beverage,
            Unit = Unit.ml,
            Weight = 500
        },
        new Item
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Сок Апельсиновый",
            CookingTime = TimeSpan.Zero,
            Price = GetRandomPrice(),
            Type = ItemType.Beverage,
            Unit = Unit.ml,
            Weight = 200
        },
        new Item
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Сок Яблочный",
            CookingTime = TimeSpan.Zero,
            Price = GetRandomPrice(),
            Type = ItemType.Beverage,
            Unit = Unit.ml,
            Weight = 200
        },
        new Item
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Сок Грейпфрутовый",
            CookingTime = TimeSpan.Zero,
            Price = GetRandomPrice(),
            Type = ItemType.Beverage,
            Unit = Unit.ml,
            Weight = 200
        },
        new Item
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Молоко",
            CookingTime = TimeSpan.Zero,
            Price = GetRandomPrice(),
            Type = ItemType.Beverage,
            Unit = Unit.ml,
            Weight = 1000
        },
        new Item
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Кока-Кола Zero",
            CookingTime = TimeSpan.Zero,
            Price = GetRandomPrice(),
            Type = ItemType.Beverage,
            Unit = Unit.ml,
            Weight = 330
        },
        new Item
        {
            Id = Guid.NewGuid().ToString(),
            Name = "7UP",
            CookingTime = TimeSpan.Zero,
            Price = GetRandomPrice(),
            Type = ItemType.Beverage,
            Unit = Unit.ml,
            Weight = 330
        },
        new Item
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Monster Energy",
            CookingTime = TimeSpan.Zero,
            Price = GetRandomPrice(),
            Type = ItemType.Beverage,
            Unit = Unit.ml,
            Weight = 500
        },
        new Item
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Сок Вишневый",
            CookingTime = TimeSpan.Zero,
            Price = GetRandomPrice(),
            Type = ItemType.Beverage,
            Unit = Unit.ml,
            Weight = 200
        },
        //wok
        new Item
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Вок с курицей и овощами",
            CookingTime = new TimeSpan(hours: 0, minutes: 15, seconds: 0),
            Price = GetRandomPrice(),
            Type = ItemType.Wok,
            Unit = Unit.piece,
            Weight = 1
        },
        new Item
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Вок с говядиной и брокколи",
            CookingTime = new TimeSpan(hours: 0, minutes: 18, seconds: 0),
            Price = GetRandomPrice(),
            Type = ItemType.Wok,
            Unit = Unit.piece,
            Weight = 1
        },
        new Item
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Вок с креветками и овощами",
            CookingTime = new TimeSpan(hours: 0, minutes: 12, seconds: 0),
            Price = GetRandomPrice(),
            Type = ItemType.Wok,
            Unit = Unit.piece,
            Weight = 1
        },
        new Item
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Вок с лососем и грибами",
            CookingTime = new TimeSpan(hours: 0, minutes: 20, seconds: 0),
            Price = GetRandomPrice(),
            Type = ItemType.Wok,
            Unit = Unit.piece,
            Weight = 1
        },
        new Item
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Вок с тофу и овощами",
            CookingTime = new TimeSpan(hours: 0, minutes: 15, seconds: 0),
            Price = GetRandomPrice(),
            Type = ItemType.Wok,
            Unit = Unit.piece,
            Weight = 1
        },
        new Item
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Вок с свининой и брокколи",
            CookingTime = new TimeSpan(hours: 0, minutes: 18, seconds: 0),
            Price = GetRandomPrice(),
            Type = ItemType.Wok,
            Unit = Unit.piece,
            Weight = 1
        },
        new Item
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Вок с уткой и грибами",
            CookingTime = new TimeSpan(hours: 0, minutes: 20, seconds: 0),
            Price = GetRandomPrice(),
            Type = ItemType.Wok,
            Unit = Unit.piece,
            Weight = 1
        },
        new Item
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Вок с кроликом и овощами",
            CookingTime = new TimeSpan(hours: 0, minutes: 22, seconds: 0),
            Price = GetRandomPrice(),
            Type = ItemType.Wok,
            Unit = Unit.piece,
            Weight = 1
        },
        new Item
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Вок с говядиной и шампиньонами",
            CookingTime = new TimeSpan(hours: 0, minutes: 18, seconds: 0),
            Price = GetRandomPrice(),
            Type = ItemType.Wok,
            Unit = Unit.piece,
            Weight = 1
        },
        new Item
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Вок с курицей и бамбуком",
            CookingTime = new TimeSpan(hours: 0, minutes: 15, seconds: 0),
            Price = GetRandomPrice(),
            Type = ItemType.Wok,
            Unit = Unit.piece,
            Weight = 1
        },

        //Kebabs
        new Item
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Кебаб из говядины",
            CookingTime = new TimeSpan(hours: 0, minutes: 25, seconds: 0),
            Price = GetRandomPrice(),
            Type = ItemType.Kebab,
            Unit = Unit.piece,
            Weight = 1
        },
        new Item
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Кебаб из курицы",
            CookingTime = new TimeSpan(hours: 0, minutes: 20, seconds: 0),
            Price = GetRandomPrice(),
            Type = ItemType.Kebab,
            Unit = Unit.piece,
            Weight = 1
        },
        new Item
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Кебаб из баранины",
            CookingTime = new TimeSpan(hours: 0, minutes: 30, seconds: 0),
            Price = GetRandomPrice(),
            Type = ItemType.Kebab,
            Unit = Unit.piece,
            Weight = 1
        },
        new Item
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Кебаб из свинины",
            CookingTime = new TimeSpan(hours: 0, minutes: 25, seconds: 0),
            Price = GetRandomPrice(),
            Type = ItemType.Kebab,
            Unit = Unit.piece,
            Weight = 1
        },
        new Item
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Кебаб из телятины",
            CookingTime = new TimeSpan(hours: 0, minutes: 30, seconds: 0),
            Price = GetRandomPrice(),
            Type = ItemType.Kebab,
            Unit = Unit.piece,
            Weight = 1
        },
        new Item
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Кебаб из кролика",
            CookingTime = new TimeSpan(hours: 0, minutes: 25, seconds: 0),
            Price = GetRandomPrice(),
            Type = ItemType.Kebab,
            Unit = Unit.piece,
            Weight = 1
        },
        new Item
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Кебаб из индейки",
            CookingTime = new TimeSpan(hours: 0, minutes: 20, seconds: 0),
            Price = GetRandomPrice(),
            Type = ItemType.Kebab,
            Unit = Unit.piece,
            Weight = 1
        },
        new Item
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Кебаб из говядины и свинины",
            CookingTime = new TimeSpan(hours: 0, minutes: 25, seconds: 0),
            Price = GetRandomPrice(),
            Type = ItemType.Kebab,
            Unit = Unit.piece,
            Weight = 1
        },
        new Item
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Кебаб из курицы и говядины",
            CookingTime = new TimeSpan(hours: 0, minutes: 20, seconds: 0),
            Price = GetRandomPrice(),
            Type = ItemType.Kebab,
            Unit = Unit.piece,
            Weight = 1
        },
        new Item
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Кебаб из баранины и курицы",
            CookingTime = new TimeSpan(hours: 0, minutes: 30, seconds: 0),
            Price = GetRandomPrice(),
            Type = ItemType.Kebab,
            Unit = Unit.piece,
            Weight = 1
        },
        new Item
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Кебаб из свинины и говядины",
            CookingTime = new TimeSpan(hours: 0, minutes: 25, seconds: 0),
            Price = GetRandomPrice(),
            Type = ItemType.Kebab,
            Unit = Unit.piece,
            Weight = 1
        },
        new Item
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Кебаб из телятины и баранины",
            CookingTime = new TimeSpan(hours: 0, minutes: 30, seconds: 0),
            Price = GetRandomPrice(),
            Type = ItemType.Kebab,
            Unit = Unit.piece,
            Weight = 1
        },
        new Item
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Кебаб из кролика и индейки",
            CookingTime = new TimeSpan(hours: 0, minutes: 25, seconds: 0),
            Price = GetRandomPrice(),
            Type = ItemType.Kebab,
            Unit = Unit.piece,
            Weight = 1
        },
        new Item
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Кебаб из говядины, курицы и свинины",
            CookingTime = new TimeSpan(hours: 0, minutes: 25, seconds: 0),
            Price = GetRandomPrice(),
            Type = ItemType.Kebab,
            Unit = Unit.piece,
            Weight = 1
        },
        new Item
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Кебаб из курицы, говядины и баранины",
            CookingTime = new TimeSpan(hours: 0, minutes: 20, seconds: 0),
            Price = GetRandomPrice(),
            Type = ItemType.Kebab,
            Unit = Unit.piece,
            Weight = 1
        },
        new Item
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Кебаб из баранины, курицы и свинины",
            CookingTime = new TimeSpan(hours: 0, minutes: 30, seconds: 0),
            Price = GetRandomPrice(),
            Type = ItemType.Kebab,
            Unit = Unit.piece,
            Weight = 1

        }
};
    }

    private static double GetRandomPrice()
    {
        Random random = new Random();
        return random.NextDouble() * 1000;
    }
}