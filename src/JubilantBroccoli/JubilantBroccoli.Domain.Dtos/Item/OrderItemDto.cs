
namespace JubilantBroccoli.Domain.Dtos.Item;

public class OrderItemDto
{
    public string Id { get; set; }
    public int Count { get; set; }
    public string[] ItemOptionIds { get; set; }
    public string RestaurantId { get; set; }
}