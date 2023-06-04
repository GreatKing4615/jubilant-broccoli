using JubilantBroccoli.Domain.Core.Enums;
using JubilantBroccoli.Domain.Dtos.Item;

namespace JubilantBroccoli.Domain.Dtos.Order;

public class OrderDto
{
    public string? DeliveryAddress { get; set; }
    public OrderStatus Status { get; set; }
    public ICollection<OrderItemDto> Items { get; set; }
    public DeliveryType DeliveryType { get; set; }
    public TimeSpan? AverageTimeToReady { get; set; }
}