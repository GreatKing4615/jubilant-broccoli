using JubilantBroccoli.Domain.Core.Interfaces;

namespace JubilantBroccoli.Domain.Models;

public class Dish: IHaveId
{
    public Guid Id { get; set; }
}