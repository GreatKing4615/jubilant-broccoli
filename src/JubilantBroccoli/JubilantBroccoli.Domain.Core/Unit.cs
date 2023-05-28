using JubilantBroccoli.Domain.Core.Interfaces;

namespace JubilantBroccoli.Domain.Models;

public class Unit: IHaveId
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}