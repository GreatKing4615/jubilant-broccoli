using JubilantBroccoli.Domain.Core.Enums;

namespace JubilantBroccoli.Domain.Dtos.Restaurant;

public class RestaurantDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public ItemType[] ItemTypes { get; set; }
    public TimeSpan Opening { get; set; }
    public TimeSpan Closing { get; set; }
}