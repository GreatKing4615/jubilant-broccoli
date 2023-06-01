using JubilantBroccoli.Domain.Core.Interfaces;

namespace JubilantBroccoli.Domain.Core.Implementations;

public class Auditable : IAuditable, IHaveId
{
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public long Id { get; set; }
}