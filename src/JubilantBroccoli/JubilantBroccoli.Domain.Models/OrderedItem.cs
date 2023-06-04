using System.Collections;
using JubilantBroccoli.Domain.Core.Implementations;

namespace JubilantBroccoli.Domain.Models;

public class OrderedItem : Auditable, IEnumerable<OrderedItem>
{
    public Order Order { get; set; }
    public Item Item { get; set; }
    public int Count { get; set; }
    public List<ItemOption> ItemOptions { get; set; }
    public IEnumerator<OrderedItem> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}