using JubilantBroccoli.BusinessLogic.Contracts;
using JubilantBroccoli.BusinessLogic.Implementations.Base;
using JubilantBroccoli.Domain.Models;
using JubilantBroccoli.Infrastructure.UnitOfWork.Contracts;
using Microsoft.Extensions.Logging;

namespace JubilantBroccoli.BusinessLogic.Implementations.Menu;

public class PizzaPreparation : OrderProcessorTemplate, IRecipe
{
    private readonly ILogger _logger;

    public PizzaPreparation(ILogger<OrderProcessorTemplate> logger, IUnitOfWork unitOfWork) : base(logger, unitOfWork)
    {
        _logger = logger;
    }

    protected override async Task PrepareIngredients(string id, OrderedItem orderedItem)
    {
        _logger.LogInformation($"order: {id}. PrepareIngredients to {orderedItem.Item.Name}");
        var timeToHeatThePan = Randomiser.GetRandomSpan();
        _logger.LogInformation($"order: {id}. Oh, it's hard for me to cut circles out of dough, but I'll give it a try: {timeToHeatThePan}");
        await Task.Delay(timeToHeatThePan);
        _logger.LogInformation($"order: {id}. will do! Time to cooking!");
    }

    //todo: подумать над реализацией непредвиденной причины для увеличения срока доставки

    protected override Task Serve(string id)
    {
        _logger.LogInformation($"Order № {id} are ready. Put on the box and send to delivery");
        return Task.CompletedTask;
    }
}