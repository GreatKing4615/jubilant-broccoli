using JubilantBroccoli.Domain.Core.Contracts;
using JubilantBroccoli.Domain.Core.Enums;

namespace JubilantBroccoli.Domain.Models
{
    public class Restaurant : IHaveId
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public ICollection<Item> Items { get; set; } = new List<Item>();
        public TimeSpan Opening { get; set; }
        public TimeSpan Closing { get; set; }
        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public List<ItemType> ItemTypes { get; set; }
    }
}