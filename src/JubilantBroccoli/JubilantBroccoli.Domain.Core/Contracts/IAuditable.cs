namespace JubilantBroccoli.Domain.Core.Contracts;

public interface IAuditable
{
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}