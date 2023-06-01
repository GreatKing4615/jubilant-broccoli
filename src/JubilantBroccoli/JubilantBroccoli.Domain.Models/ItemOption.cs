using JubilantBroccoli.Domain.Core.Interfaces;

namespace JubilantBroccoli.Domain.Models;

public class ItemOption: IHaveId
{
    public string Name { get; set; }
    public long Id { get; set; }
    public ICollection<Item> Items { get; set; }
}