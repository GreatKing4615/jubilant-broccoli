﻿using JubilantBroccoli.BusinessLogic.Contracts;
using JubilantBroccoli.BusinessLogic.Implementations.Base;
using JubilantBroccoli.Domain.Models;
using JubilantBroccoli.Infrastructure.UnitOfWork.Contracts;
using Microsoft.Extensions.Logging;

namespace JubilantBroccoli.BusinessLogic.Implementations.Menu;

public class WokPreparation : OrderProcessorTemplate, IRecipe
{
    private readonly ILogger _logger;

    public WokPreparation(ILogger<OrderProcessorTemplate> logger, IUnitOfWork unitOfWork) : base(logger, unitOfWork)
    {
        _logger = logger;
    }

    protected override async Task PrepareIngredients(string id, OrderedItem orderedItem)
    {
        _logger.LogInformation($"order: {id}. PrepareIngredients to {orderedItem.Item.Name}");
        var timeToHeatThePan = Randomiser.GetRandomSpan();
        _logger.LogInformation($"order: {id}. Need a little bit more time to heat the pan: {timeToHeatThePan}");
        await Task.Delay(timeToHeatThePan);
        _logger.LogInformation($"order: {id}. Watch out! it's hot! Time to cooking!");
    }

    protected override Task Serve(string id)
    {
        _logger.LogInformation($"Order № {id} are ready. Put on the box and send to delivery");
        return Task.CompletedTask;
    }
}