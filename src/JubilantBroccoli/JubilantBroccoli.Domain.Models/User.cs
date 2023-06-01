using JubilantBroccoli.Domain.Core.Contracts;

namespace JubilantBroccoli.Domain.Models;

public class User: IHaveId
{
    public string? Address { get; set; }
    public ICollection<Order> Orders { get; set; }
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
}