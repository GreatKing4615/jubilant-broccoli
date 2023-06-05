using JubilantBroccoli.Domain.Core.Enums;

namespace JubilantBroccoli.Domain.Dtos.Item;

public class ItemOptionDtoResponse
{
    public string Name { get; set; }
    public string Id { get; set; }
    public ItemType Type { get; set; }
}