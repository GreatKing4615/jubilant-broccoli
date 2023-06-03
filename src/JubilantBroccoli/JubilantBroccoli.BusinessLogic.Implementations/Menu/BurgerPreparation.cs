using JubilantBroccoli.BusinessLogic.Implementations.Base;
using JubilantBroccoli.Domain.Models;
using Microsoft.Extensions.Logging;

namespace JubilantBroccoli.BusinessLogic.Implementations.Menu;

public class BurgerPreparation : ItemPreparationTemplate
{
    private readonly ILogger _logger;

    public BurgerPreparation(ILogger<BurgerPreparation> logger) : base(logger)
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
        _logger.LogInformation($"Order № {id} are ready. Put on the box and send to delivery");
        return Task.CompletedTask;
    }
}