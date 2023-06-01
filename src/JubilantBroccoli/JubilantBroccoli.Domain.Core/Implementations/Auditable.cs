using JubilantBroccoli.Domain.Core.Contracts;

namespace JubilantBroccoli.Domain.Core.Implementations;

public class Auditable : IAuditable, IHaveId
{
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid Id { get; set; }
}