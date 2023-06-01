namespace JubilantBroccoli.Domain.Models;

public class Customer: Person
{
    public string? Address { get; set; }
    public IEnumerable<Order> Orders { get; set; }
}