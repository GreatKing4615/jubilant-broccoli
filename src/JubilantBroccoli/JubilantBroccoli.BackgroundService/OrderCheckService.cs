using JubilantBroccoli.BusinessLogic.Contracts;
using JubilantBroccoli.Domain.Core.Enums;
using JubilantBroccoli.Domain.Models;
using JubilantBroccoli.Infrastructure.UnitOfWork.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace JubilantBroccoli.BackgroundService;

public class OrderCheckService
{
    private readonly ILogger<OrderCheckService> _logger;
    private readonly IRepository<Order> _orderRepository;
    private readonly IOrderProcessor _orderProcessor;

    public OrderCheckService(ILogger<OrderCheckService> logger, IUnitOfWork unitOfWork, IOrderProcessor orderProcessor)
    {
        _logger = logger;
        _orderRepository = unitOfWork.GetRepository<Order>();
        _orderProcessor = orderProcessor;
    }

    public async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("OrderCheckService is running.");

            var cookingOrders = _orderRepository.GetAll(
                selector: x => x,
                predicate: x => new[] { OrderStatus.Cooking, OrderStatus.Delivering }.Contains(x.Status),
                include: include => include
                    .Include(o => o.OrderedItems)
                    .ThenInclude(oi => oi.Item)
                    .Include(o => o.OrderedItems)
                    .ThenInclude(oi => oi.ItemOptions)
            ).ToList();

            foreach (var order in cookingOrders)
            {
                await _orderProcessor.ProcessOrder(order!);
            }
        }
        _logger.LogInformation("OrderCheckService stopped.");
    }
}