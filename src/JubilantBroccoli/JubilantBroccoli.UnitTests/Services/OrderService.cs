using System.Linq.Expressions;
using JubilantBroccoli.BusinessLogic.Contracts;
using JubilantBroccoli.BusinessLogic.Implementations;
using JubilantBroccoli.Domain.Core.CustomExceptions;
using JubilantBroccoli.Domain.Core.Enums;
using JubilantBroccoli.Domain.Models;
using JubilantBroccoli.Infrastructure.UnitOfWork.Contracts;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace JubilantBroccoli.UnitTests.Services
{
    public class OrderServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<ILogger<OrderService>> _loggerMock;
        private readonly Mock<IRepository<Order>> _orderRepositoryMock;
        private readonly Mock<IRepository<ItemOption>> _itemOptionRepositoryMock;
        private readonly Mock<IRepository<Item>> _itemRepositoryMock;
        private readonly Mock<IRepository<User>> _userRepositoryMock;

        private readonly IOrderService _orderService;

        public OrderServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _loggerMock = new Mock<ILogger<OrderService>>();
            _orderRepositoryMock = new Mock<IRepository<Order>>();
            _itemOptionRepositoryMock = new Mock<IRepository<ItemOption>>();
            _itemRepositoryMock = new Mock<IRepository<Item>>();
            _userRepositoryMock = new Mock<IRepository<User>>();

            _unitOfWorkMock.Setup(uow => uow.GetRepository<Order>()).Returns(_orderRepositoryMock.Object);
            _unitOfWorkMock.Setup(uow => uow.GetRepository<ItemOption>()).Returns(_itemOptionRepositoryMock.Object);
            _unitOfWorkMock.Setup(uow => uow.GetRepository<Item>()).Returns(_itemRepositoryMock.Object);
            _unitOfWorkMock.Setup(uow => uow.GetRepository<User>()).Returns(_userRepositoryMock.Object);

            _orderService = new OrderService(_unitOfWorkMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task GetCurrentCartAsync_UserHasCart_ReturnsExistingCart()
        {
            // Arrange
            var userId = "1";
            var existingCart = new Order
            {
                Id = "cart1",
                User = new User { Id = userId },
                Status = OrderStatus.InTheCart,
                OrderedItems = new List<OrderedItem>()
            };
            _orderRepositoryMock.Setup(repo => repo.SingleOrDefault<Order>(
                    It.IsAny<Expression<Func<Order, Order>>>(),
                    It.IsAny<Expression<Func<Order, bool>>>(),
                    null,
                    It.IsAny<Func<IQueryable<Order>, IIncludableQueryable<Order, object>>>(),
                    true,
                    CancellationToken.None,
                    false,
                    false))
                .ReturnsAsync(existingCart);

            // Act
            var result = await _orderService.GetCurrentCartAsync(userId);

            // Assert
            Assert.Equal(existingCart, result);
        }

        [Fact]
        public async Task GetCurrentCartAsync_UserDoesNotHaveCart_CreatesNewCart()
        {
            // Arrange
            var userId = "1";
            _userRepositoryMock.Setup(repo => repo.SingleOrDefault<User>(
                It.IsAny<Expression<Func<User, User>>>(),
                It.IsAny<Expression<Func<User, bool>>>(),
                null,
                It.IsAny<Func<IQueryable<User>, IIncludableQueryable<User, object>>>(),
                true,
                CancellationToken.None,
                false,
                false)).ReturnsAsync(new User
                {
                    Id = userId
                });
            _orderRepositoryMock.Setup(repo => repo.SingleOrDefault<Order>(
                It.IsAny<Expression<Func<Order, Order>>>(),
                It.IsAny<Expression<Func<Order, bool>>>(),
                null,
                It.IsAny<Func<IQueryable<Order>, IIncludableQueryable<Order, object>>>(),
                true,
                CancellationToken.None,
                false,
                false)).ReturnsAsync((Order)null);

            // Act
            var result = await _orderService.GetCurrentCartAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userId, result.User.Id);
            Assert.Equal(OrderStatus.InTheCart, result.Status);
        }

        [Fact]
        public async Task AddToCartAsync_ItemExistsInCart_IncrementsItemCount()
        {
            // Arrange
            var userId = "1";
            var restaurantId = "restaurant1";
            var itemId = "item1";
            var itemOptions = new string[] { "option1", "option2" };
            var initialItemCount = 2;

            var existingItem = new Item { Id = itemId };
            var existingOptions = new List<ItemOption>
            {
                new ItemOption { Id = "option1" },
                new ItemOption { Id = "option2" }
            };

            var existingCart = new Order
            {
                Id = "cart1",
                User = new User { Id = userId },
                Status = OrderStatus.InTheCart,
                OrderedItems = new List<OrderedItem>
                {
                    new OrderedItem
                    {
                        Item = existingItem,
                        ItemOptions = existingOptions,
                        Count = initialItemCount
                    }
                }
            };

            _orderRepositoryMock.Setup(repo => repo.SingleOrDefault<Order>(
                It.IsAny<Expression<Func<Order, Order>>>(),
                It.IsAny<Expression<Func<Order, bool>>>(),
                null,
                It.IsAny<Func<IQueryable<Order>, IIncludableQueryable<Order, object>>>(),
                true,
                CancellationToken.None,
                false,
                false))
                .ReturnsAsync(existingCart);

            // Act
            var result = await _orderService.AddToCartAsync(userId, restaurantId, 1, itemId, itemOptions);

            // Assert
            Assert.Equal(existingCart, result);
            Assert.Single(result.OrderedItems);
            Assert.Equal(initialItemCount + 1, result.OrderedItems.First().Count);
        }

        [Fact]
        public async Task AddToCartAsync_ItemDoesNotExistInCart_AddsNewItem()
        {
            // Arrange
            var userId = "1";
            var restaurantId = "restaurant1";
            var itemId = "item1";
            var itemOptions = new string[] { "option1", "option2" };
            var itemCount = 2;

            var newItem = new Item { Id = itemId };
            var newOptions = new List<ItemOption>
            {
                new ItemOption { Id = "option1" },
                new ItemOption { Id = "option2" }
            };

            var existingCart = new Order
            {
                Id = "cart1",
                User = new User { Id = userId },
                Status = OrderStatus.InTheCart,
                OrderedItems = new List<OrderedItem>()
            };

            _orderRepositoryMock.Setup(repo => repo.SingleOrDefault<Order>(
                It.IsAny<Expression<Func<Order, Order>>>(),
                It.IsAny<Expression<Func<Order, bool>>>(),
                null,
                It.IsAny<Func<IQueryable<Order>, IIncludableQueryable<Order, object>>>(),
                true,
                CancellationToken.None,
                false,
                false)).ReturnsAsync(existingCart);

            _itemRepositoryMock.Setup(repo => repo.GetFirstOrDefaultAsync<Item>(
                    It.IsAny<Expression<Func<Item, Item>>>(),
                    It.IsAny<Expression<Func<Item, bool>>>(),
                    null,
                    null,
                    true,
                    CancellationToken.None,
                    false,
                    false))
                .ReturnsAsync(newItem);

            var firstCall = true;
            _itemOptionRepositoryMock.Setup(repo => repo.GetFirstOrDefaultAsync<ItemOption>(
                    It.IsAny<Expression<Func<ItemOption, ItemOption>>>(),
                    It.IsAny<Expression<Func<ItemOption, bool>>>(),
                    null,
                    null,
                    true,
                    CancellationToken.None,
                    false,
                    false))
                .ReturnsAsync(() =>
                {
                    if (firstCall)
                    {
                        firstCall = false;
                        return newOptions[0];
                    }
                    else
                    {
                        return newOptions[1];
                    }
                });

            // Act
            var result = await _orderService.AddToCartAsync(userId, restaurantId, itemCount, itemId, itemOptions);

            // Assert
            Assert.Equal(existingCart, result);
            Assert.Single(result.OrderedItems);
            Assert.Equal(newItem, result.OrderedItems.First().Item);
            Assert.Equal(newOptions, result.OrderedItems.First().ItemOptions);
            Assert.Equal(itemCount, result.OrderedItems.First().Count);
        }

        [Fact]
        public async Task RemoveFromCartAsync_ItemExistsInCart_DecrementsItemCount()
        {
            // Arrange
            var userId = "1";
            var itemId = "item1";
            var initialItemCount = 2;

            var existingItem = new Item { Id = itemId };

            var existingCart = new Order
            {
                Id = "cart1",
                User = new User { Id = userId },
                Status = OrderStatus.InTheCart,
                OrderedItems = new List<OrderedItem>
                {
                    new OrderedItem
                    {
                        Item = existingItem,
                        ItemOptions = new List<ItemOption>(),
                        Count = initialItemCount
                    }
                }
            };

            _orderRepositoryMock.Setup(repo => repo.SingleOrDefault<Order>(
                It.IsAny<Expression<Func<Order, Order>>>(),
                It.IsAny<Expression<Func<Order, bool>>>(),
                null,
                It.IsAny<Func<IQueryable<Order>, IIncludableQueryable<Order, object>>>(),
                true,
                CancellationToken.None,
                false,
                false))
                .ReturnsAsync(existingCart);

            // Act
            var result = await _orderService.RemoveFromCartAsync(userId, itemId);

            // Assert
            Assert.Equal(existingCart, result);
            Assert.Single(result.OrderedItems);
            Assert.Equal(initialItemCount - 1, result.OrderedItems.First().Count);
        }

        [Fact]
        public async Task RemoveFromCartAsync_ItemDoesNotExistInCart_ThrowsException()
        {
            // Arrange
            var userId = "1";
            var itemId = "item1";

            var existingCart = new Order
            {
                Id = "cart1",
                User = new User { Id = userId },
                Status = OrderStatus.InTheCart,
                OrderedItems = new List<OrderedItem>()
            };

            _orderRepositoryMock.Setup(repo => repo.SingleOrDefault<Order>(
                It.IsAny<Expression<Func<Order, Order>>>(),
                It.IsAny<Expression<Func<Order, bool>>>(),
                null,
                It.IsAny<Func<IQueryable<Order>, IIncludableQueryable<Order, object>>>(),
                true,
                CancellationToken.None,
                false,
                false))
                .ReturnsAsync(existingCart);

            // Act and Assert
            await Assert.ThrowsAsync<Exception>(() => _orderService.RemoveFromCartAsync(userId, itemId));
        }

        [Fact]
        public async Task ClearCartAsync_CartExists_RemovesAllItemsFromCart()
        {
            // Arrange
            var orderId = "cart1";

            var existingCart = new Order
            {
                Id = orderId,
                User = new User(),
                Status = OrderStatus.InTheCart,
                OrderedItems = new List<OrderedItem>
                {
                    new OrderedItem(),
                    new OrderedItem()
                }
            };

            _orderRepositoryMock.Setup(repo => repo.SingleOrDefault<Order>(
                It.IsAny<Expression<Func<Order, Order>>>(),
                It.IsAny<Expression<Func<Order, bool>>>(),
                null,
                It.IsAny<Func<IQueryable<Order>, IIncludableQueryable<Order, object>>>(),
                true,
                CancellationToken.None,
                false,
                false))
                .ReturnsAsync(existingCart);

            // Act
            var result = await _orderService.ClearCartAsync(orderId);

            // Assert
            Assert.Equal(existingCart, result);
            Assert.Empty(result.OrderedItems);
        }

        [Fact]
        public async Task ClearCartAsync_CartDoesNotExist_ThrowsException()
        {
            // Arrange
            var orderId = "cart1";

            _orderRepositoryMock.Setup(repo => repo.SingleOrDefault<Order>(
                It.IsAny<Expression<Func<Order, Order>>>(),
                It.IsAny<Expression<Func<Order, bool>>>(),
                null,
                It.IsAny<Func<IQueryable<Order>, IIncludableQueryable<Order, object>>>(),
                true,
                CancellationToken.None,
                false,
                false))
                .ReturnsAsync((Order)null);

            // Act and Assert
            await Assert.ThrowsAsync<Exception>(() => _orderService.ClearCartAsync(orderId));
        }

        [Fact]
        public async Task ChangeOrderStatusAsync_TargetStatusIsCanceled_ChangesStatusToCanceled()
        {
            // Arrange
            var orderId = "order1";
            var targetStatus = OrderStatus.Canceled;

            var existingOrder = new Order
            {
                Id = orderId,
                User = new User(),
                Status = OrderStatus.WaitingPay
            };

            _orderRepositoryMock.Setup(repo => repo.SingleOrDefault<Order>(
                It.IsAny<Expression<Func<Order, Order>>>(),
                It.IsAny<Expression<Func<Order, bool>>>(),
                null,
                It.IsAny<Func<IQueryable<Order>, IIncludableQueryable<Order, object>>>(),
                true,
                CancellationToken.None,
                false,
                false))
                .ReturnsAsync(existingOrder);

            // Act
            var result = await _orderService.ChangeOrderStatusAsync(orderId, targetStatus);

            // Assert
            Assert.Equal(existingOrder, result);
            Assert.Equal(targetStatus, result.Status);
        }

        [Fact]
        public async Task ChangeOrderStatusAsync_TargetStatusIsFinished_ThrowsIncorrectStatusException()
        {
            // Arrange
            var orderId = "order1";
            var targetStatus = OrderStatus.Finished;

            var existingOrder = new Order
            {
                Id = orderId,
                User = new User(),
                Status = OrderStatus.InTheCart
            };

            _orderRepositoryMock.Setup(repo => repo.SingleOrDefault<Order>(
                It.IsAny<Expression<Func<Order, Order>>>(),
                It.IsAny<Expression<Func<Order, bool>>>(),
                null,
                It.IsAny<Func<IQueryable<Order>, IIncludableQueryable<Order, object>>>(),
                true,
                CancellationToken.None,
                false,
                false))
                .ReturnsAsync(existingOrder);

            // Act and Assert
            await Assert.ThrowsAsync<IncorrectStatusException>(() => _orderService.ChangeOrderStatusAsync(orderId, targetStatus));
        }
        [Fact]
        public async Task RemoveFromCartAsync_WhenOnlyOneItemInCart_DecreaseCountAndSaveChanges()
        {
            // Arrange
            var userId = "user1";
            var itemId = "item1";
            var token = CancellationToken.None;

            var cart = new Order
            {
                Id = "cart1",
                Status = OrderStatus.InTheCart,
                OrderedItems = new List<OrderedItem>
            {
                new OrderedItem
                {
                    Item = new Item { Id = itemId },
                    Count = 1,
                    ItemOptions = new List<ItemOption>()
                }
            }
            };

            _orderRepositoryMock.Setup(repo => repo.SingleOrDefault<Order>(
                    It.IsAny<Expression<Func<Order, Order>>>(),
                    It.IsAny<Expression<Func<Order, bool>>>(),
                    null,
                    It.IsAny<Func<IQueryable<Order>, IIncludableQueryable<Order, object>>>(),
                    true,
                    CancellationToken.None,
                    false,
                    false))
                .ReturnsAsync(cart);

            _unitOfWorkMock.Setup(uow => uow.SaveChangesAsync(token))
                .ReturnsAsync(1);

            var orderService = new OrderService(
                _unitOfWorkMock.Object,
                _loggerMock.Object);

            // Act
            var result = await orderService.RemoveFromCartAsync(userId, itemId, token);

            // Assert
            Assert.Equal(0, result.OrderedItems.Count);
            _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(token), Times.Once);
        }

        [Fact]
        public async Task RemoveFromCartAsync_WhenMultipleItemsInCart_RemoveItemAndSaveChanges()
        {
            // Arrange
            var userId = "user1";
            var itemId = "item1";
            var token = CancellationToken.None;

            var cart = new Order
            {
                Id = "cart1",
                Status = OrderStatus.InTheCart,
                OrderedItems = new List<OrderedItem>
            {
                new OrderedItem
                {
                    Item = new Item { Id = itemId },
                    Count = 2,
                    ItemOptions = new List<ItemOption>()
                }
            }
            };

            _orderRepositoryMock.Setup(repo => repo.SingleOrDefault<Order>(
                    It.IsAny<Expression<Func<Order, Order>>>(),
                    It.IsAny<Expression<Func<Order, bool>>>(),
                    null,
                    It.IsAny<Func<IQueryable<Order>, IIncludableQueryable<Order, object>>>(),
                    true,
                    CancellationToken.None,
                    false,
                    false))
                .ReturnsAsync(cart);

            _unitOfWorkMock.Setup(uow => uow.SaveChangesAsync(token))
                .ReturnsAsync(1);

            var orderService = new OrderService(
                _unitOfWorkMock.Object,
                _loggerMock.Object);

            // Act
            var result = await orderService.RemoveFromCartAsync(userId, itemId, token);

            // Assert
            Assert.NotEmpty(result.OrderedItems);
            Assert.Equal(1, result.OrderedItems.Count);
            Assert.Equal(1, result.OrderedItems.First().Count);
            _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(token), Times.Once);
        }

    }
}
