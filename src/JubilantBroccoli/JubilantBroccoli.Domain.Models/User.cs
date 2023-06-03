using JubilantBroccoli.Domain.Core.Contracts;
using Microsoft.AspNetCore.Identity;

namespace JubilantBroccoli.Domain.Models;

public class User : IdentityUser, IHaveId
{
    public string? Address { get; set; }
    public ICollection<Order> Orders { get; set; }
    public string Role { get; set; }
}