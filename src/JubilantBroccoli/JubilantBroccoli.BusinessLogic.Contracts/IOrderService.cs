using JubilantBroccoli.Domain.Core.Enums;
using JubilantBroccoli.Domain.Models;
using JubilantBroccoli.Infrastructure.UnitOfWork.Contracts;

namespace JubilantBroccoli.BusinessLogic.Contracts;

public interface IOrderService
{
    public Task<Order> GetCurrentCartAsync(string userId, CancellationToken token = default);
    public Task<IPagedList<Order>> GetOrderHistoryAsync(string userId, CancellationToken token = default);
    public Task<Order> AddToCartAsync(string userId, string restaurantId, int count, string itemId, string[] itemOptions, CancellationToken token = default);
    public Task<Order> RemoveFromCartAsync(string userId, string itemId, CancellationToken token = default);
    public Task<Order> ClearCartAsync(string orderId, CancellationToken token = default);
    public Task<Order> ChangeOrderStatusAsync(string orderId, OrderStatus targetStatus, CancellationToken token = default);
}