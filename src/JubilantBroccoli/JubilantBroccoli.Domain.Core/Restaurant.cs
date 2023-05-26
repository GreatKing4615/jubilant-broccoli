using JubilantBroccoli.Domain.Core.Interfaces;

namespace JubilantBroccoli.Domain.Models
{
    public class Restaurant: IHaveId
    {
        public Guid Id { get; set; }
    }
}