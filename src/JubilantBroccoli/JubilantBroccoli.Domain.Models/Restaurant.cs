using JubilantBroccoli.Domain.Core.Enums;
using JubilantBroccoli.Domain.Core.Interfaces;

namespace JubilantBroccoli.Domain.Models
{
    public class Restaurant: IHaveId
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public ICollection<Item> Dishes { get; set; }
        public ICollection<ItemType> DishTypes { get; set; }
        public TimeSpan Opening { get; set; }
        public TimeSpan Closing { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}