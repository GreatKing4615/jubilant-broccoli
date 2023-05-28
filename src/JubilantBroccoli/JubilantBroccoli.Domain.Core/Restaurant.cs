using JubilantBroccoli.Domain.Core.Enums;
using JubilantBroccoli.Domain.Core.Interfaces;

namespace JubilantBroccoli.Domain.Models
{
    public class Restaurant: IHaveId
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Dish> Dishes { get; set; }
        public ICollection<DishType> DishTypes { get; set; }
        public TimeSpan Opening { get; set; }
        public TimeSpan Closing { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}