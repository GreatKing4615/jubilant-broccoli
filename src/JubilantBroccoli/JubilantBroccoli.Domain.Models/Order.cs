using JubilantBroccoli.Domain.Core.Enums;
using JubilantBroccoli.Domain.Core.Implementations;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;

namespace JubilantBroccoli.Domain.Models;

public class Order : Auditable
{
    public IdentityUser User { get; set; }
    public string? DeliveryAddress { get; set; }
    public List<OrderedItem> OrderedItems { get; set; } = new();
    
    public TimeSpan DeliveryTime { get; set; }
    public string? RestaurantId { get; set; }
    public Restaurant Restaurant { get; set; }

    [JsonConverter(typeof(StringEnumConverter))]
    public DeliveryType DeliveryType { get; set; } = DeliveryType.PickUp;

    [JsonConverter(typeof(StringEnumConverter))]
    public OrderStatus Status { get; set; }
}