using JubilantBroccoli.Domain.Core.Interfaces;

namespace JubilantBroccoli.Domain.Models
{
    public class Person: IHaveId
    {
        public long Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
