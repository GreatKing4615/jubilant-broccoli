using JubilantBroccoli.BusinessLogic.Contracts;
using JubilantBroccoli.Domain.Core.Enums;
using JubilantBroccoli.Domain.Models;
using JubilantBroccoli.Infrastructure.UnitOfWork.Contracts;
using Microsoft.Extensions.Logging;

namespace JubilantBroccoli.BusinessLogic.Implementations.Base;

public abstract class ItemPreparationTemplate : IOrderProcessor
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ItemPreparationTemplate> _logger;
    private readonly IRepository<Order> _orderRepository;
    protected readonly IRepository<OrderedItem> _orderedItemRepository;

    protected ItemPreparationTemplate(ILogger<ItemPreparationTemplate> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _orderRepository = unitOfWork.GetRepository<Order>();
        _orderedItemRepository = unitOfWork.GetRepository<OrderedItem>();
    }

    public async Task ProcessOrder(Order order)
    {
        order.Status = OrderStatus.Cooking;
        _orderRepository.Update(order);
        var remainingDishes = order.OrderedItems.Where(x => x.Status != ItemStatus.Ready).ToList();
        foreach (var item in remainingDishes)
        {
            var recipeBook = new RecipeBook(_logger, _unitOfWork);
            var recipe = recipeBook.GetRecipe(item.Item.Type);
            await recipe.CookByRecipe(order.Id, item);
        }
        await Delivery(order, order.DeliveryType, order.DeliveryTime, order.DeliveryAddress);
    }

    /// <summary>
    /// default script for cooking all dishes
    /// </summary>
    public async Task CookByRecipe(string orderId, OrderedItem orderItem)
    {
        await PrepareIngredients(orderId, orderItem);
        orderItem.Status = ItemStatus.Cooking;
        _orderedItemRepository.Update(orderItem);
        await Cook(orderId, orderItem);
        await AddOptions(orderId, orderItem.Item.Name, orderItem.ItemOptions.ToArray());
        await Serve(orderId);
        orderItem.Status = ItemStatus.Ready;
        _orderedItemRepository.Update(orderItem);
    }

    protected abstract Task PrepareIngredients(string id, OrderedItem orderedItem);
    protected abstract Task Serve(string id);

    protected async Task AddOptions(string id, string orderName, ItemOption[] itemOptions)
    {
        foreach (var item in itemOptions)
        {
            _logger.LogInformation($"Order № {id}. Add {item.Name} to {orderName}");
            await Task.Delay(Randomiser.GetRandomSpan(max: 10000));
        }
    }

    protected async Task Cook(string id, OrderedItem orderedItem)
    {
        _logger.LogInformation($"Order № {id}. Cooking to {orderedItem.Item.Name}");
        await Task.Delay(orderedItem.Item.CookingTime);
    }

    public async Task Delivery(Order order, DeliveryType deliveryType, TimeSpan? deliveryTime, string? address)
    {
        _logger.LogInformation($"Order № {order.Id}; Delivery type: {deliveryType}; Average delivery time: {deliveryTime}; Address: {address}");
        if (deliveryType == DeliveryType.Courier)
        {
            var timeToDelivery = Randomiser.GetRandomSpan(200000, 500000);
            order.Status = OrderStatus.Delivering;
            _orderRepository.Update(order);
            _logger.LogInformation($"Order № {order.Id}; vroom vroom! Courier will delivery order in average");
            await Task.Delay(timeToDelivery);
            order.Status = OrderStatus.Finished;
            _orderRepository.Update(order);
            _logger.LogInformation($"Order № {order.Id} was delivered. Finish!");
        }
        else
        {
            order.Status = OrderStatus.WaitingPickup;
            _orderRepository.Update(order);
            _logger.LogInformation($"Order № {order.Id}; Ready to self-pickup. Note: Customer  should click to \"Finish\" button");
        }
    }
}