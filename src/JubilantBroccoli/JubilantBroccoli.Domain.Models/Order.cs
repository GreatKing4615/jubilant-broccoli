using JubilantBroccoli.Domain.Core.Enums;
using JubilantBroccoli.Domain.Core.Implementations;
using Microsoft.AspNetCore.Identity;

namespace JubilantBroccoli.Domain.Models;

public class Order : Auditable
{
    public string? UserId { get; set; }
    public IdentityUser User { get; set; }
    public string? DeliveryAddress { get; set; }
    public List<OrderedItem> OrderedItems { get; set; } = new();
    public TimeSpan DeliveryTime { get; set; }
    public string? RestaurantId { get; set; }
    public Restaurant Restaurant { get; set; }
    public DeliveryType DeliveryType { get; set; } = DeliveryType.PickUp;
    public OrderStatus Status { get; set; }
}