
namespace JubilantBroccoli.Domain.Dtos.Item;

public class OrderItemDto
{
    public long Id { get; set; }
    public int Count { get; set; }
    public IEnumerable<long> ItemOptionIds { get; set; }
    public long RestaurantId { get; set; }
}