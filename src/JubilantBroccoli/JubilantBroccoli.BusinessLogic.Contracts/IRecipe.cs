using JubilantBroccoli.Domain.Models;

namespace JubilantBroccoli.BusinessLogic.Contracts;

public interface IRecipe
{
    public Task CookByRecipe(string orderId, OrderedItem orderItem);
}