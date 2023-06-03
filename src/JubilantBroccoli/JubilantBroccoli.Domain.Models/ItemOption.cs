using JubilantBroccoli.Domain.Core.Contracts;
using JubilantBroccoli.Domain.Core.Enums;

namespace JubilantBroccoli.Domain.Models;

public class ItemOption : IHaveId
{
    public string Id { get; set; }
    public string Name { get; set; }
    public ItemType Type { get; set; }
    public ICollection<Item> Items { get; set; }
}