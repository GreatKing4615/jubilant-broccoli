using JubilantBroccoli.Domain.Models;

namespace JubilantBroccoli.BusinessLogic.Contracts;

public interface IOrderProcessor
{
    public Task ProcessOrder(Order order);
}