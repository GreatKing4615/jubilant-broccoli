using JubilantBroccoli.BusinessLogic.Contracts;
using JubilantBroccoli.BusinessLogic.Implementations;
using JubilantBroccoli.Domain.Core.Enums;
using JubilantBroccoli.Domain.Models;
using JubilantBroccoli.Infrastructure.Core.Base;
using JubilantBroccoli.Infrastructure.UnitOfWork.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace JubilantBroccoli.BackgroundService;

public class MockFlowService
{
    private readonly ILogger<OrderCheckService> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IOrderService _orderService;
    private string? _myPokemonId;
    private readonly ApplicationDbContext _context;
    private readonly IRepository<User> _userRepository;

    public MockFlowService(
        ILogger<OrderCheckService> logger,
        IServiceProvider serviceProvider,
        IUnitOfWork unitOfWork,
        IOrderService orderService)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _orderService = orderService;
        var scope = serviceProvider.CreateScope();
        _context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        _userRepository = _unitOfWork.GetRepository<User>();
    }

    public async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _myPokemonId ??= await _userRepository.SingleOrDefault(
            selector: x => x.Id,
            predicate: x => x.UserName == "TestPokemon",
            cancellationToken: stoppingToken);
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Pokemon want to it.");
            await CreateOrder(_myPokemonId);
        }
        _logger.LogInformation("OrderCheckService stopped.");
    }

    public async Task CreateOrder(string userId)
    {
        if (_myPokemonId == null)
        {
            throw new Exception($"User with ID {userId} does not exist.");
        }

        var order = await _orderService.GetCurrentCartAsync(_myPokemonId);
        _logger.LogInformation($"Current cart - {order.OrderedItems.Count} elements");

        var rest = (await _unitOfWork.GetRepository<Restaurant>().GetPagedListAsync(
            selector: x => x,
            include: x => x.Include(x => x.Items).ThenInclude(x => x.ItemOptions)
        )).Items.MinBy(x => Guid.NewGuid());

        var items = rest.Items.OrderBy(x => Guid.NewGuid()).Take(Randomiser.GetRandomNumber(1, 5)).ToList();

        _logger.LogInformation($"Pokemon choose - {rest.Name} and {string.Join(",", items.Select(x => x.Name))}");
        foreach (var item in items)
        {
            var itemsOptions = item.ItemOptions.OrderBy(x => Guid.NewGuid()).Take(Randomiser.GetRandomNumber(5)).Select(x => x.Id).ToArray();
            await _orderService.AddToCartAsync(_myPokemonId, rest.Id, Randomiser.GetRandomNumber(5), item.Id, itemsOptions);
        }

        _logger.LogInformation($"Pokemon want to order %)");

        await _orderService.ChangeOrderStatusAsync(order.Id, OrderStatus.WaitingPay);
        await _orderService.ChangeOrderStatusAsync(order.Id, OrderStatus.Cooking);
    }
}