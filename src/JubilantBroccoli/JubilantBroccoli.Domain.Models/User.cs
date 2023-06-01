namespace JubilantBroccoli.Domain.Models;

public class User: Person
{
    public string? Address { get; set; }
    public ICollection<Order> Orders { get; set; }
}