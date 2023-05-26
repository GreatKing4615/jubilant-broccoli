using JubilantBroccoli.Domain.Core.Interfaces;

namespace JubilantBroccoli.Domain.Models;

public class Cart: IHaveId
{
    public Guid Id { get; set; }
}