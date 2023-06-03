using JubilantBroccoli.BusinessLogic.Implementations.Base;
using JubilantBroccoli.Domain.Models;
using Microsoft.Extensions.Logging;

namespace JubilantBroccoli.BusinessLogic.Implementations.Menu;

public class SushiPreparation : ItemPreparationTemplate
{
    private readonly ILogger _logger;

    public SushiPreparation(ILogger<SushiPreparation> logger) : base(logger)
    {
        _logger = logger;
    }

    protected override async Task PrepareIngredients(string id, OrderedItem orderedItem)
    {
        _logger.LogInformation($"order: {id}. PrepareIngredients to {orderedItem.Item.Name}");
        var timeToFishing = Randomiser.GetRandomSpan();
        _logger.LogInformation($"order: {id}. Fishing will spend {timeToFishing}");
        await Task.Delay(timeToFishing);
        _logger.LogInformation($"order: {id}. Good fish! Time to cooking!");
    }

    protected override Task Serve(string id)
    {
        _logger.LogInformation($"Order № {id} are ready. Send to delivery");
        return Task.CompletedTask;
    }
}