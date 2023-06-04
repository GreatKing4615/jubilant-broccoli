using JubilantBroccoli.BusinessLogic.Contracts;
using JubilantBroccoli.Domain.Core.CustomExceptions;
using JubilantBroccoli.Domain.Core.Enums;
using JubilantBroccoli.Domain.Models;
using JubilantBroccoli.Infrastructure.UnitOfWork.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace JubilantBroccoli.BusinessLogic.Implementations;

public class OrderService : IOrderService
{
    private readonly IRepository<Order> _orderRepository;
    private readonly IRepository<Item> _itemRepository;
    private readonly IRepository<ItemOption> _itemOptionRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<OrderService> _logger;

    public OrderService(IUnitOfWork unitOfWork, ILogger<OrderService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _orderRepository = unitOfWork.GetRepository<Order>();
        _itemRepository = unitOfWork.GetRepository<Item>();
        _itemOptionRepository = unitOfWork.GetRepository<ItemOption>();
        _userRepository = unitOfWork.GetRepository<User>();
        _logger.LogInformation($"{nameof(OrderService)} are ready");
    }

    public async Task<Order> GetCurrentCartAsync(string userId, CancellationToken token = default)
    {
        var cart = await _orderRepository.SingleOrDefault(
            selector: x => x,
            predicate: o => o.User.Id == userId && o.Status == OrderStatus.InTheCart,
            include: include => include
                .Include(o => o.OrderedItems)
                    .ThenInclude(oi => oi.Item)
                .Include(o => o.OrderedItems)
                    .ThenInclude(oi => oi.ItemOptions),
            cancellationToken: token
        );

        if (cart == null)
        {
            _logger.LogInformation($"User {userId}, doesn't have a cert. Try to create it");
            cart = await CreateCart(userId, token);

            await _orderRepository.InsertAsync(cart, token);
        }

        return cart;
    }

    public async Task<IPagedList<Order>> GetOrderHistoryAsync(string userId, CancellationToken token = default)
    {
        var orderHistory = await _orderRepository.GetPagedListAsync(
            selector: x => x,
            predicate: o => o.User.Id == userId && o.Status != OrderStatus.InTheCart,
            include: include => include
                .Include(o => o.OrderedItems)
                    .ThenInclude(oi => oi.Item)
                .Include(o => o.OrderedItems)
                    .ThenInclude(oi => oi.ItemOptions)
                        .ThenInclude(io => io.Items),
            cancellationToken: token
        );

        return orderHistory;
    }

    public async Task<Order> AddToCartAsync(string userId, string restaurantId, int count, string itemId, string[] itemOptions, CancellationToken token = default)
    {
        var currentCart = await GetCurrentCartAsync(userId, token);

        var existingOrderedItem = currentCart.OrderedItems.FirstOrDefault(
            oi => oi.Item.Id == itemId && oi.ItemOptions.Select(x => x.Id).SequenceEqual(itemOptions));

        if (existingOrderedItem != null)
        {
            existingOrderedItem.Count += count;
        }
        else
        {
            var item = await _itemRepository.GetFirstOrDefaultAsync(
                selector: x => x,
                predicate: x => x.Id == itemId,
                cancellationToken: token);
            var itemOptionList = new List<ItemOption>();

            foreach (var itemOptionId in itemOptions)
            {
                var itemOption = await _itemOptionRepository.GetFirstOrDefaultAsync(
                    selector: x => x,
                    predicate: x => x.Id == itemOptionId,
                    cancellationToken: token);
                _logger.LogWarning($"ItemOption {itemOption} is null");
                itemOptionList.Add(itemOption);
            }

            var newOrderedItem = new OrderedItem
            {
                Item = item,
                ItemOptions = itemOptionList,
                Count = count
            };

            currentCart.OrderedItems.Add(newOrderedItem);
        }

        await _unitOfWork.SaveChangesAsync(token);

        return currentCart;
    }
    public async Task<Order> RemoveFromCartAsync(string userId, string itemId, CancellationToken token = default)
    {
        var cart = await GetCurrentCartAsync(userId, token);
        if (cart == null)
        {
            throw new Exception("Cart not found.");
        }

        var orderedItem = cart.OrderedItems.FirstOrDefault(oi => oi.Item.Id == itemId);
        if (orderedItem == null)
        {
            throw new Exception("Item not found in the cart.");
        }

        if (orderedItem.Count > 1)
        {
            orderedItem.Count--;
        }
        else
        {
            foreach (var itemOption in orderedItem.ItemOptions)
            {
                orderedItem.ItemOptions.Remove(itemOption);
            }

            if (!orderedItem.ItemOptions.Any())
            {
                cart.OrderedItems.Remove(orderedItem);
            }
            else
            {
                throw new Exception("ItemOptions still reference to Item. Remove itemOptions before Delete");
            }

        }
        await _unitOfWork.SaveChangesAsync(token);


        return cart;
    }

    public async Task<Order> ClearCartAsync(string orderId, CancellationToken token = default)
    {
        var cart = await _orderRepository.SingleOrDefault(
            selector: x => x,
            predicate: o => o.Id == orderId && o.Status == OrderStatus.InTheCart,
            include: include => include.Include(o => o.OrderedItems),
            cancellationToken: token
        );

        if (cart == null)
        {
            throw new Exception("Cart not found.");
        }

        cart.OrderedItems.Clear();

        await _unitOfWork.SaveChangesAsync(token);

        return cart;
    }

    public async Task<Order> ChangeOrderStatusAsync(string orderId, OrderStatus targetStatus, CancellationToken token = default)
    {
        var allowedStatus = new List<OrderStatus> { OrderStatus.Canceled };
        switch (targetStatus)
        {
            case OrderStatus.Canceled:
                allowedStatus = new List<OrderStatus> { OrderStatus.WaitingPay};
                break;
            case OrderStatus.Finished:
                allowedStatus = new List<OrderStatus> { OrderStatus.Cooking, OrderStatus.Delivering};
                break;
            case OrderStatus.WaitingPay:
                allowedStatus = new List<OrderStatus> { OrderStatus.InTheCart };
                break;
            default: throw new IncorrectStatusException($"Order {orderId} try to set incorrect status = {targetStatus}");
        }
        var order = await _orderRepository.SingleOrDefault(
            selector: x => x,
            predicate: x => x.Id == orderId,
            cancellationToken: token
        );
        if (order == null)
        {
            throw new Exception("Order not found.");
        }
        if (!allowedStatus.Contains(order.Status))
        {
            var wrongStatus = $"Order {order.Id} has incorrect status = {order.Status}";
            _logger.LogWarning(wrongStatus);
            throw new IncorrectStatusException(wrongStatus);
        }

        order.Status = targetStatus;

        await _unitOfWork.SaveChangesAsync(token);

        return order;
    }

    private async Task<Order> CreateCart(string userId, CancellationToken token = default)
    {
        var currentUser = await _userRepository.SingleOrDefault(
            selector: x => x,
            predicate: x => x.Id == userId,
            cancellationToken: token
        );
        if (currentUser is null)
        {
            throw new Exception($"User with id = {userId} unknown");
        }

        var cart = new Order
        {
            User = currentUser,
            Status = OrderStatus.InTheCart,
            OrderedItems = new List<OrderedItem>()
        };
        return cart;
    }
}
