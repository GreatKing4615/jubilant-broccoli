using JubilantBroccoli.Domain.Core.Enums;
using JubilantBroccoli.Domain.Core.Implementations;

namespace JubilantBroccoli.Domain.Models;

public class Order : Auditable
{
    public User User { get; set; }
    public string? DeliveryAddress { get; set; }
    public OrderStatus Status { get; set; }
    public ICollection<OrderedItem> OrderedItems { get; set; }
    public DeliveryType DeliveryType { get; set; }
    public TimeSpan DeliveryTime { get; set; }
    public Restaurant Restaurant { get; set; }
}