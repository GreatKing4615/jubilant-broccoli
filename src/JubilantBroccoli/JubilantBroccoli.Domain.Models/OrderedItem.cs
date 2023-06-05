using JubilantBroccoli.Domain.Core.Enums;
using JubilantBroccoli.Domain.Core.Implementations;

namespace JubilantBroccoli.Domain.Models;

public class OrderedItem : Auditable
{
    public string? OrderId { get; set; }
    public Order Order { get; set; }
    public Item Item { get; set; }
    public string? ItemId { get; set; }
    public int Count { get; set; }
    public List<ItemOption> ItemOptions { get; set; }
    public ItemStatus Status { get; set; }
}