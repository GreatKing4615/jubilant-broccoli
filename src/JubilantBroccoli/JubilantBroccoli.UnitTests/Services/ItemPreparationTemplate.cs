using JubilantBroccoli.BusinessLogic.Implementations.Base;
using JubilantBroccoli.BusinessLogic.Implementations.Menu;
using JubilantBroccoli.Domain.Core.Enums;
using JubilantBroccoli.Domain.Models;
using JubilantBroccoli.Infrastructure.UnitOfWork.Contracts;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace JubilantBroccoli.UnitTests.Services
{
    public class OrderProcessorTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<ILogger<OrderProcessorTemplate>> _loggerMock;
        private readonly Mock<IRepository<Order>> _orderRepositoryMock;
        private readonly Mock<IRepository<OrderedItem>> _orderedItemRepositoryMock;

        public OrderProcessorTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _loggerMock = new Mock<ILogger<OrderProcessorTemplate>>();
            _orderRepositoryMock = new Mock<IRepository<Order>>();
            _orderedItemRepositoryMock = new Mock<IRepository<OrderedItem>>();
            _unitOfWorkMock.Setup(uow => uow.GetRepository<Order>()).Returns(_orderRepositoryMock.Object);
            _unitOfWorkMock.Setup(uow => uow.GetRepository<OrderedItem>()).Returns(_orderedItemRepositoryMock.Object);
            _orderRepositoryMock.Setup(repo => repo.Update(
                It.IsAny<Order>()));
            _orderedItemRepositoryMock.Setup(repo => repo.Update(
                It.IsAny<OrderedItem>()));
        }

        [Fact]
        public async Task ProcessOrder_UpdatesOrderStatusAndCallsCookByRecipeForEachItem()
        {
            // Arrange
            var order = new Order();
            var orderedItem = new OrderedItem
            {
                Order = order,
                Item = new Item
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Маргарита",
                    CookingTime = new TimeSpan(hours: 0, minutes: 0, seconds: 2),
                    Price = 1,
                    Type = ItemType.Pizza,
                    Unit = Unit.piece,
                    Weight = 1
                },
                Count = 2,
                Status = ItemStatus.Pending,
                ItemOptions = new List<ItemOption>
                {
                     new ItemOption
                    {
                        Id = "123",
                        Name = "Example Name",
                        Type = ItemType.Pizza
                    }

                }
            };
            order.OrderedItems.Add(orderedItem);
            var template = new Mock<OrderProcessorTemplate>(_loggerMock.Object, _unitOfWorkMock.Object) { CallBase = true }.Object;


            // Act
            await template.ProcessOrder(order);

            // Assert
            Assert.Equal(OrderStatus.WaitingPickup, order.Status);
            Assert.Equal(ItemStatus.Ready, orderedItem.Status);
            _unitOfWorkMock.Verify(u => u.GetRepository<Order>().Update(order), Times.Exactly(2));
            _unitOfWorkMock.Verify(u => u.GetRepository<OrderedItem>().Update(orderedItem), Times.Exactly(2));
        }


        [Fact]
        public async Task ProcessOrder_UpdatesOrderStatusAndCallsCookByRecipeForEachItem_Pizza()
        {
            // Arrange
            var order = new Order();
            var orderedItem = new OrderedItem
            {
                Order = order,
                Item = new Item
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Маргарита",
                    CookingTime = new TimeSpan(hours: 0, minutes: 0, seconds: 2),
                    Price = 1,
                    Type = ItemType.Pizza,
                    Unit = Unit.piece,
                    Weight = 1
                },
                Count = 2,
                Status = ItemStatus.Pending,
                ItemOptions = new List<ItemOption>
            {
                 new ItemOption
                {
                    Id = "123",
                    Name = "Example Name",
                    Type = ItemType.Pizza
                }
            }
            };
            order.OrderedItems.Add(orderedItem);
            var template = new Mock<OrderProcessorTemplate>(_loggerMock.Object, _unitOfWorkMock.Object) { CallBase = true }.Object;

            // Act
            await template.ProcessOrder(order);

            // Assert
            Assert.Equal(OrderStatus.WaitingPickup, order.Status);
            Assert.Equal(ItemStatus.Ready, orderedItem.Status);
            _unitOfWorkMock.Verify(u => u.GetRepository<Order>().Update(order), Times.Exactly(2));
            _unitOfWorkMock.Verify(u => u.GetRepository<OrderedItem>().Update(orderedItem), Times.Exactly(2));
        }

        [Fact]
        public async Task ProcessOrder_UpdatesOrderStatusAndCallsCookByRecipeForEachItem_Drink()
        {
            // Arrange
            var order = new Order();
            var orderedItem = new OrderedItem
            {
                Order = order,
                Item = new Item
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Cola",
                    CookingTime = new TimeSpan(hours: 0, minutes: 0, seconds: 0),
                    Price = 1,
                    Type = ItemType.Beverage,
                    Unit = Unit.ml,
                    Weight = 1
                },
                Count = 1,
                Status = ItemStatus.Pending,
                ItemOptions = new List<ItemOption>()
            };
            order.OrderedItems.Add(orderedItem);
            var template = new Mock<OrderProcessorTemplate>(_loggerMock.Object, _unitOfWorkMock.Object) { CallBase = true }.Object;

            // Act
            await template.ProcessOrder(order);

            // Assert
            Assert.Equal(OrderStatus.WaitingPickup, order.Status);
            Assert.Equal(ItemStatus.Ready, orderedItem.Status);
            _unitOfWorkMock.Verify(u => u.GetRepository<Order>().Update(order), Times.Exactly(2));
            _unitOfWorkMock.Verify(u => u.GetRepository<OrderedItem>().Update(orderedItem), Times.Exactly(2));
        }
        [Fact]
        public async Task ProcessOrder_UpdatesOrderStatusAndCallsCookByRecipeForEachItem_Kebab()
        {
            // Arrange
            var order = new Order();
            var orderedItem = new OrderedItem
            {
                Order = order,
                Item = new Item
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Chicken Kebab",
                    CookingTime = new TimeSpan(hours: 0, minutes: 0, seconds: 5),
                    Price = 2,
                    Type = ItemType.Kebab,
                    Unit = Unit.piece,
                    Weight = 1
                },
                Count = 1,
                Status = ItemStatus.Pending,
                ItemOptions = new List<ItemOption>()
            };
            order.OrderedItems.Add(orderedItem);
            var template = new Mock<OrderProcessorTemplate>(_loggerMock.Object, _unitOfWorkMock.Object) { CallBase = true }.Object;

            // Act
            await template.ProcessOrder(order);

            // Assert
            Assert.Equal(OrderStatus.WaitingPickup, order.Status);
            Assert.Equal(ItemStatus.Ready, orderedItem.Status);
            _unitOfWorkMock.Verify(u => u.GetRepository<Order>().Update(order), Times.Exactly(2));
            _unitOfWorkMock.Verify(u => u.GetRepository<OrderedItem>().Update(orderedItem), Times.Exactly(2));
        }

        [Fact]
        public async Task ProcessOrder_UpdatesOrderStatusAndCallsCookByRecipeForEachItem_Wok()
        {
            // Arrange
            var order = new Order();
            var orderedItem = new OrderedItem
            {
                Order = order,
                Item = new Item
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Vegetable Wok",
                    CookingTime = new TimeSpan(hours: 0, minutes: 0, seconds: 3),
                    Price = 3,
                    Type = ItemType.Wok,
                    Unit = Unit.piece,
                    Weight = 1
                },
                Count = 1,
                Status = ItemStatus.Pending,
                ItemOptions = new List<ItemOption>()
            };
            order.OrderedItems.Add(orderedItem);
            var template = new Mock<OrderProcessorTemplate>(_loggerMock.Object, _unitOfWorkMock.Object) { CallBase = true }.Object;

            // Act
            await template.ProcessOrder(order);

            // Assert
            Assert.Equal(OrderStatus.WaitingPickup, order.Status);
            Assert.Equal(ItemStatus.Ready, orderedItem.Status);
            _unitOfWorkMock.Verify(u => u.GetRepository<Order>().Update(order), Times.Exactly(2));
            _unitOfWorkMock.Verify(u => u.GetRepository<OrderedItem>().Update(orderedItem), Times.Exactly(2));
        }

        [Fact]
        public async Task ProcessOrder_UpdatesOrderStatusAndCallsCookByRecipeForEachItem_Burger()
        {
            // Arrange
            var order = new Order();
            var orderedItem = new OrderedItem
            {
                Order = order,
                Item = new Item
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Cheeseburger",
                    CookingTime = new TimeSpan(hours: 0, minutes: 0, seconds: 4),
                    Price = 4,
                    Type = ItemType.Burger,
                    Unit = Unit.piece,
                    Weight = 1
                },
                Count = 1,
                Status = ItemStatus.Pending,
                ItemOptions = new List<ItemOption>()
            };
            order.OrderedItems.Add(orderedItem);
            var template = new Mock<OrderProcessorTemplate>(_loggerMock.Object, _unitOfWorkMock.Object) { CallBase = true }.Object;

            // Act
            await template.ProcessOrder(order);

            // Assert
            Assert.Equal(OrderStatus.WaitingPickup, order.Status);
            Assert.Equal(ItemStatus.Ready, orderedItem.Status);
            _unitOfWorkMock.Verify(u => u.GetRepository<Order>().Update(order), Times.Exactly(2));
            _unitOfWorkMock.Verify(u => u.GetRepository<OrderedItem>().Update(orderedItem), Times.Exactly(2));
        }

        [Fact]
        public async Task ProcessOrder_UpdatesOrderStatusAndCallsCookByRecipeForEachItem_Sushi()
        {
            // Arrange
            var order = new Order();
            var orderedItem = new OrderedItem
            {
                Order = order,
                Item = new Item
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Salmon Sushi",
                    CookingTime = new TimeSpan(hours: 0, minutes: 0, seconds: 2),
                    Price = 5,
                    Type = ItemType.Sushi,
                    Unit = Unit.piece,
                    Weight = 1
                },
                Count = 1,
                Status = ItemStatus.Pending,
                ItemOptions = new List<ItemOption>()
            };
            order.OrderedItems.Add(orderedItem);
            var template = new Mock<OrderProcessorTemplate>(_loggerMock.Object, _unitOfWorkMock.Object) { CallBase = true }.Object;

            // Act
            await template.ProcessOrder(order);

            // Assert
            Assert.Equal(OrderStatus.WaitingPickup, order.Status);
            Assert.Equal(ItemStatus.Ready, orderedItem.Status);
            _unitOfWorkMock.Verify(u => u.GetRepository<Order>().Update(order), Times.Exactly(2));
            _unitOfWorkMock.Verify(u => u.GetRepository<OrderedItem>().Update(orderedItem), Times.Exactly(2));
        }

    }
}
