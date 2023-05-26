using JubilantBroccoli.Domain.Core.Interfaces;

namespace JubilantBroccoli.Domain.Core.Implementations;

public class Identity : IHaveId
{
    public Guid Id { get; set; }
}