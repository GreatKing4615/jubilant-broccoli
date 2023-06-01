using JubilantBroccoli.Domain.Core.Enums;
using JubilantBroccoli.Domain.Core.Interfaces;

namespace JubilantBroccoli.Domain.Models
{
    public class Restaurant: IHaveId
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public ICollection<Item> Items { get; set; }
        public ICollection<ItemType> ItemTypes { get; set; }
        public TimeSpan Opening { get; set; }
        public TimeSpan Closing { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}