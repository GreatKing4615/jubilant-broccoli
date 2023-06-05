using JubilantBroccoli.Domain.Core.Contracts;
using JubilantBroccoli.Domain.Core.Enums;

namespace JubilantBroccoli.Domain.Models;

public class Item : IHaveId
{
    public string Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public TimeSpan CookingTime { get; set; }
    public double Weight { get; set; }
    public ICollection<ItemOption> ItemOptions { get; set; }
    public ICollection<Restaurant> Restaurants { get; set; }
    public Unit Unit { get; set; }
    public ItemType Type { get; set; }
}