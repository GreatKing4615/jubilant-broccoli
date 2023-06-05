using JubilantBroccoli.Domain.Core.Enums;

namespace JubilantBroccoli.Domain.Dtos.Item;

public class OrderItemDtoResponse
{
    public string Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public double Weight { get; set; }
    public TimeSpan CookingTime { get; set; }
    public Unit Unit { get; set; }
    public ItemType Type { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public List<ItemOptionDtoResponse> ItemOptions { get; set; }
}