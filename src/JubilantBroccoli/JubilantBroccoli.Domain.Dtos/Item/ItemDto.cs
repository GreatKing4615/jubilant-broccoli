using JubilantBroccoli.Domain.Core.Enums;

namespace JubilantBroccoli.Domain.Dtos.Item;

public class ItemDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public double Weight { get; set; }
    public Unit Unit { get; set; }
    public ItemType Type { get; set; }
    public IEnumerable<ItemOptionDto> Options { get; set; }
}