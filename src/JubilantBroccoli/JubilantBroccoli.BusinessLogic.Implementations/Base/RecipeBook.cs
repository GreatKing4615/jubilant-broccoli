using JubilantBroccoli.BusinessLogic.Contracts;
using JubilantBroccoli.BusinessLogic.Implementations.Menu;
using JubilantBroccoli.Domain.Core.Enums;
using JubilantBroccoli.Infrastructure.UnitOfWork.Contracts;
using Microsoft.Extensions.Logging;

namespace JubilantBroccoli.BusinessLogic.Implementations.Base;

public class RecipeBook
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ItemPreparationTemplate> _logger;

    public RecipeBook(ILogger<ItemPreparationTemplate> logger, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public IRecipe GetRecipe(ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.Burger:
                return new BurgerPreparation(_logger, _unitOfWork);
            case ItemType.Kebab:
                return new KebabPreparation(_logger, _unitOfWork);
            case ItemType.Pizza:
                return new PizzaPreparation(_logger, _unitOfWork);
            case ItemType.Sushi:
                return new SushiPreparation(_logger, _unitOfWork);
            case ItemType.Wok:
                return new WokPreparation(_logger, _unitOfWork);
            case ItemType.Beverage:
                return new BeveragePreparation(_logger, _unitOfWork);
            default:
                throw new ArgumentException("Invalid item type.");
        }
    }
}