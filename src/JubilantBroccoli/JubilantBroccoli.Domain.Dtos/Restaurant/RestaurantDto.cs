using JubilantBroccoli.Domain.Dtos.Item;

namespace JubilantBroccoli.Domain.Dtos.Restaurant;

public class RestaurantDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ICollection<ItemDto> Items { get; set; }
    public TimeSpan Opening { get; set; }
    public TimeSpan Closing { get; set; }
    // public ICollection<OrderDto> Orders { get; set; }

}