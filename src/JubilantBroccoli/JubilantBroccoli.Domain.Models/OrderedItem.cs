using JubilantBroccoli.Domain.Core.Enums;
using JubilantBroccoli.Domain.Core.Implementations;

namespace JubilantBroccoli.Domain.Models;

public class OrderedItem : Auditable
{
    public Order Order { get; set; }
    public Item Item { get; set; }
    public int Count { get; set; }
    public ItemStatus Status { get; set; }
    public List<ItemOption> ItemOptions { get; set; }
}