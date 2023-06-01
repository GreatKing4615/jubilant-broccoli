using JubilantBroccoli.Domain.Core.Enums;
using JubilantBroccoli.Domain.Core.Implementations;

namespace JubilantBroccoli.Domain.Models;

/// <summary>
/// 
/// </summary>
public class Item : Auditable
{
    public string Name { get; set; }
    public double Price { get; set; }
    public TimeSpan CookingTime { get; set; }
    public double Weight { get; set; }
    public Unit Unit { get; set; }
    public ItemType Type { get; set; }
    public ICollection<ItemOption> ItemOptions { get; set; }
}