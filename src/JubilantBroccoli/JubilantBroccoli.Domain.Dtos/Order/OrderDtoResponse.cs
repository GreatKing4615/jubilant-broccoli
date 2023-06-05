using JubilantBroccoli.Domain.Core.Enums;
using JubilantBroccoli.Domain.Dtos.Item;
using JubilantBroccoli.Domain.Dtos.Restaurant;

namespace JubilantBroccoli.Domain.Dtos.Order;

public class OrderDtoResponse
{
    public string Id { get; set; }
    public string? DeliveryAddress { get; set; }
    public OrderStatus Status { get; set; }
    public IEnumerable<OrderItemDtoResponse> Items { get; set; }
    public DeliveryType DeliveryType { get; set; }
    public RestaurantDtoResponse Restaurant { get; set; }
    public TimeSpan? AverageTimeToReady { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}