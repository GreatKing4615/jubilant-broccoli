using JubilantBroccoli.BusinessLogic.Contracts;
using JubilantBroccoli.BusinessLogic.Implementations.Base;
using JubilantBroccoli.Domain.Models;
using JubilantBroccoli.Infrastructure.UnitOfWork.Contracts;
using Microsoft.Extensions.Logging;

namespace JubilantBroccoli.BusinessLogic.Implementations.Menu;

public class KebabPreparation : OrderProcessorTemplate, IRecipe
{
    private readonly ILogger _logger;
    private readonly string[] _candidatesToMeat = { "Cow", "Pig", "Rabbit", "Sheep", "Cat", "Dog", "chicken", "turkey" };

    public KebabPreparation(ILogger<OrderProcessorTemplate> logger, IUnitOfWork unitOfWork) : base(logger, unitOfWork)
    {
        _logger = logger;
    }

    protected override async Task PrepareIngredients(string id, OrderedItem orderedItem)
    {
        _logger.LogInformation($"order: {id}. PrepareIngredients to {orderedItem.Item.Name}");
        var timeToCatchKittens = Randomiser.GetRandomSpan();
        _logger.LogInformation($"order: {id}. Need time for catch the meat. Will spend {timeToCatchKittens}");
        await Task.Delay(timeToCatchKittens);
        _logger.LogInformation(
            $"order: {id}. Woohoo! {_candidatesToMeat[Randomiser.GetRandomNumber(_candidatesToMeat.Length)]} was catch. Kebab will be!"
            );
    }

    protected override async Task Serve(string id)
    {
        _logger.LogInformation($"Order № {id} almost ready. Just wrap it in precious parchment...");
        await Task.Delay(Randomiser.GetRandomSpan());
        _logger.LogInformation($"Order № {id} are ready. Send to delivery");
    }
}