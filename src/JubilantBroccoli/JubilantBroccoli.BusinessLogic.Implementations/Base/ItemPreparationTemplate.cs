using JubilantBroccoli.Domain.Core.Enums;
using JubilantBroccoli.Domain.Models;
using Microsoft.Extensions.Logging;

namespace JubilantBroccoli.BusinessLogic.Implementations.Base;

public abstract class ItemPreparationTemplate
{
    private readonly ILogger<ItemPreparationTemplate> _logger;

    protected ItemPreparationTemplate(ILogger<ItemPreparationTemplate> logger)
    {
        _logger = logger;
    }

    public async Task WorkOnItemTemplate(Order order)
    {
        foreach (var item in order.OrderedItems)
        {
            await CookItemTemplate(order.Id, item);
        }
        await Delivery(order.Id, order.DeliveryType, order.DeliveryTime, order.DeliveryAddress);
    }

    /// <summary>
    /// default script for cooking all dishes
    /// </summary>
    public async Task CookItemTemplate(string orderId, OrderedItem orderItem)
    {
        await PrepareIngredients(orderId, orderItem);
        await Cook(orderId, orderItem);
        await AddOptions(orderId, orderItem.Item.Name, orderItem.ItemOptions.ToArray());
        await Serve(orderId);
    }

    protected abstract Task PrepareIngredients(string id, OrderedItem orderedItem);
    protected abstract Task Serve(string id);

    protected async Task AddOptions(string id, string orderName, ItemOption[] itemOptions)
    {
        foreach (var item in itemOptions)
        {
            _logger.LogInformation($"order: {id}. Add {item.Name} to {orderName}");
            await Task.Delay(Randomiser.GetRandomSpan(max: 10000));
        }
    }

    protected async Task Cook(string id, OrderedItem orderedItem)
    {
        _logger.LogInformation($"order: {id}. Cooking to {orderedItem.Item.Name}");
        await Task.Delay(orderedItem.Item.CookingTime);
    }

    public async Task Delivery(string id, DeliveryType deliveryType, TimeSpan? deliveryTime, string? address)
    {
        _logger.LogInformation($"order: {id}; Delivery type: {deliveryType}; Average delivery time: {deliveryTime}; Address: {address}");
        if (deliveryType == DeliveryType.Courier)
        {
            var timeToDelivery = Randomiser.GetRandomSpan(200000, 500000);
            _logger.LogInformation($"order: {id}; vroom vroom! Courier will delivery order in average");
            await Task.Delay(timeToDelivery);
        }
    }
}