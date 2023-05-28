using JubilantBroccoli.Domain.Core.Interfaces;

namespace JubilantBroccoli.Domain.Core.Implementations;

public class Auditable : IAuditable
{
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}