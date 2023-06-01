using JubilantBroccoli.Domain.Core.Implementations;

namespace JubilantBroccoli.Domain.Models;

public class OrderedItem: Auditable
{
    public Order Order { get; set; }
    public Item Item { get; set; }
    public ICollection<ItemOption> ItemOptions { get; set; }
}