using JubilantBroccoli.Domain.Core.Enums;
using JubilantBroccoli.Domain.Core.Implementations;
using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;

namespace JubilantBroccoli.Domain.Models;

public class OrderedItem : Auditable
{
    public Order Order { get; set; }
    public Item Item { get; set; }
    public string? ItemId { get; set; }
    public int Count { get; set; }
    public List<ItemOption> ItemOptions { get; set; }

    [JsonConverter(typeof(StringEnumConverter))]
    public ItemStatus Status { get; set; }
}