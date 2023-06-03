using JubilantBroccoli.Domain.Core.Contracts;

namespace JubilantBroccoli.Domain.Models;

public class ItemOption : IHaveId
{
    public string Name { get; set; }
    public string Id { get; set; }
    public ICollection<Item> Items { get; set; }
}