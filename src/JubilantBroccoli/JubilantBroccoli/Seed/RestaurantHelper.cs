using JubilantBroccoli.Domain.Core.Enums;
using JubilantBroccoli.Domain.Models;

namespace JubilantBroccoli.Seed;

public static class RestaurantHelper
{
    public static List<Restaurant> GetRestaurants()
    {
        return new List<Restaurant>
        {
            new Restaurant
            {
                Id = Guid.NewGuid().ToString(),
                Address = "г.Оренбург, ул. Пушкина, 123",
                Closing = new TimeSpan(hours:22,minutes:00,seconds:00),
                Opening = new TimeSpan(hours:9,minutes:00,seconds:00),
                ItemTypes = new List<ItemType> {ItemType.Sushi, ItemType.Wok, ItemType.Beverage},
                Name = "Ёбидоеби"
            },
            new Restaurant
            {
                Id = Guid.NewGuid().ToString(),
                Address = "г.Оренбург, ул. Пушкина, 124",
                Closing = new TimeSpan(hours:22,minutes:00,seconds:00),
                Opening = new TimeSpan(hours:9,minutes:00,seconds:00),
                ItemTypes = new List<ItemType> {ItemType.Wok, ItemType.Burger, ItemType.Beverage},
                Name = "2Woks1Burger"
            },
            new Restaurant
            {
                Id = Guid.NewGuid().ToString(),
                Address = "г.Оренбург, ул. Пушкина, 125",
                Closing = new TimeSpan(hours:23,minutes:59,seconds:59),
                Opening = new TimeSpan(hours:0,minutes:00,seconds:00),
                ItemTypes = new List<ItemType> {ItemType.Kebab,ItemType.Beverage},
                Name = "Самокрутка"
            },
            new Restaurant
            {
                Id = Guid.NewGuid().ToString(),
                Address = "г.Оренбург, ул. Пушкина, 126",
                Closing = new TimeSpan(hours:22,minutes:00,seconds:00),
                Opening = new TimeSpan(hours:10,minutes:00,seconds:00),
                ItemTypes = new List<ItemType> {ItemType.Pizza, ItemType.Beverage},
                Name = "MammaMia"
            }
        };
    }
}