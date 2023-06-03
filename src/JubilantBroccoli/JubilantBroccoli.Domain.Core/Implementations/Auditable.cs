using JubilantBroccoli.Domain.Core.Contracts;

namespace JubilantBroccoli.Domain.Core.Implementations;

public class Auditable : IAuditable, IHaveId
{
    public string Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}