using JubilantBroccoli.Domain.Core.Enums;
using JubilantBroccoli.Domain.Dtos.Item;
using JubilantBroccoli.Domain.Dtos.Restaurant;

namespace JubilantBroccoli.Domain.Dtos.Order;

public class OrderDto
{
    public string Id { get; set; }
    public string? DeliveryAddress { get; set; }
    public OrderStatus Status { get; set; }
    public List<OrderItemDto> Items { get; set; }
    public DeliveryType DeliveryType { get; set; }
    public RestaurantDto Restaurant { get; set; }
    public TimeSpan? AverageTimeToReady { get; set; }
}