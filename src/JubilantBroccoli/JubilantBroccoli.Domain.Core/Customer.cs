using JubilantBroccoli.Domain.Core.Interfaces;

namespace JubilantBroccoli.Domain.Models;

public class Customer: IHaveId
{
    public Guid Id { get; set; }
}