using JubilantBroccoli.BusinessLogic.Contracts;
using JubilantBroccoli.BusinessLogic.Implementations.Base;
using JubilantBroccoli.Domain.Models;
using JubilantBroccoli.Infrastructure.UnitOfWork.Contracts;
using Microsoft.Extensions.Logging;

namespace JubilantBroccoli.BusinessLogic.Implementations.Menu;

public class BeveragePreparation : ItemPreparationTemplate, IRecipe
{
    private readonly ILogger _logger;

    public BeveragePreparation(ILogger<ItemPreparationTemplate> logger, IUnitOfWork unitOfWork) : base(logger, unitOfWork)
    {
        _logger = logger;
    }

    protected override Task PrepareIngredients(string id, OrderedItem orderedItem)
    {
        _logger.LogInformation($"order: {id}. PrepareIngredients to {orderedItem.Item.Name}. But we have in the fridge, we don't spend any time.");
        return Task.CompletedTask;
    }

    protected override Task Serve(string id)
    {
        _logger.LogInformation($"Order № {id} are ready. Put on the box and that's all");
        return Task.CompletedTask;
    }
}