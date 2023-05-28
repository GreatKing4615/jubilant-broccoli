using JubilantBroccoli.Domain.Core.Interfaces;

namespace JubilantBroccoli.Domain.Models;

public class Addon: IHaveId
{
    public string Name { get; set; }
    public Guid Id { get; set; }
    public ICollection<Dish> Dish { get; set; }
}