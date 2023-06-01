using JubilantBroccoli.Domain.Core.Enums;
using JubilantBroccoli.Domain.Core.Implementations;

namespace JubilantBroccoli.Domain.Models;

public class Order: Auditable
{
    public Customer Customer { get; set; }
    public string? DeliveryAddress { get; set; }
    public OrderStatus Status { get; set; }
    public ICollection<Item> Items { get; set; }
    public DeliverType DeliverType { get; set; }
}