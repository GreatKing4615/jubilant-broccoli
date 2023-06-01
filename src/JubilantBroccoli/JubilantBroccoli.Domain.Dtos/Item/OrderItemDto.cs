
namespace JubilantBroccoli.Domain.Dtos.Item;

public class OrderItemDto
{
    public Guid Id { get; set; }
    public int Count { get; set; }
    public IEnumerable<Guid> ItemOptionIds { get; set; }
    public Guid RestaurantId { get; set; }
}