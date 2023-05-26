using JubilantBroccoli.Domain.Core.Interfaces;

namespace JubilantBroccoli.Domain.Core.Implementations;

public class Auditable : Identity, IAuditable
{
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string UpdatedBy { get; set; }
}