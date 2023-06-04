using JubilantBroccoli.Domain.Core.Enums;
using JubilantBroccoli.Domain.Core.Implementations;

namespace JubilantBroccoli.Domain.Models;

public class Order : Auditable
{
    public User User { get; set; }
    public string? DeliveryAddress { get; set; }
    public OrderStatus Status { get; set; }
    public List<OrderedItem> OrderedItems { get; set; } = new();
    public DeliveryType DeliveryType { get; set; } = DeliveryType.PickUp; 
    public TimeSpan DeliveryTime { get; set; }
    public Restaurant Restaurant { get; set; }
}