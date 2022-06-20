using Autofac.Extras.Moq;
using AutoMapper;
using Moq;
using NUnit.Framework;
using Shoppers.Storage.BusinessObjects;
using Shoppers.Storage.Repositories;
using Shoppers.Storage.Services;
using Shoppers.Storage.UnitOfWorks;
using Shouldly;

using OrderEO = Shoppers.Storage.Entities.Order;

namespace Shoppers.Storage.Test.Services
{
    public class OrderServiceTest
    {
        private AutoMock _mock;
        private Mock<IStoreUnitOfWork> _storeUnitOfWorkMock;
        private Mock<IOrderRepository> _orderRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private IOrderService _orderService;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _mock = AutoMock.GetLoose();
        }

        [SetUp]
        public void Setup()
        {
            _storeUnitOfWorkMock = _mock.Mock<IStoreUnitOfWork>();
            _orderRepositoryMock = _mock.Mock<IOrderRepository>();
            _mapperMock = _mock.Mock<IMapper>();
            _orderService = _mock.Create<OrderService>();
        }

        [TearDown]
        public void TearDown()
        {
            _storeUnitOfWorkMock.Reset();
            _orderRepositoryMock.Reset();
            _mapperMock.Reset();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _mock.Dispose();
        }

        [Test]
        public void GetOrder_ValidId_ReturnsOrder()
        {
            // Arrange
            var id = 5;

            OrderEO orderEntity = new OrderEO
            {
                Id = 5
            };
            Order order = new Order() { Id = orderEntity.Id };

            _storeUnitOfWorkMock.Setup(x => x.Orders)
                .Returns(_orderRepositoryMock.Object);

            _orderRepositoryMock.Setup(x => x.GetById(id)).Returns(orderEntity);

            _mapperMock.Setup(x => x.Map<Order>(orderEntity))
                .Returns(order);

            // Act
            var result = _orderService.GetOrder(id);

            // Assert
            this.ShouldSatisfyAllConditions(
                () => result.ShouldNotBeNull(),
                () => result.Id.ShouldBe(id)
            );
        }

        [Test]
        public void EditOrder_ValidId_Order()
        {
            // Arrange
            var id = 5;

            OrderEO orderEntity = new OrderEO
            {
                Id = 5
            };
            Order order = new Order() { Id = orderEntity.Id };

            _storeUnitOfWorkMock.Setup(x => x.Orders)
                .Returns(_orderRepositoryMock.Object);

            _orderRepositoryMock.Setup(x => x.GetById(id)).Returns(orderEntity);

            _mapperMock.Setup(x => x.Map<Order>(orderEntity))
                .Returns(order);
            _storeUnitOfWorkMock.Setup(x => x.Save()).Verifiable();
            // Act
            _orderService.EditOrder(order);

            // Assert
            _storeUnitOfWorkMock.VerifyAll();
        }

        [Test]
        public void DeleteOrder_ValidId_Order()
        {
            // Arrange
            var id = 5;

            OrderEO orderEntity = new OrderEO
            {
                Id = 5
            };
            Order order = new Order() { Id = orderEntity.Id };

            _storeUnitOfWorkMock.Setup(x => x.Orders)
               .Returns(_orderRepositoryMock.Object);

            _orderRepositoryMock.Setup(x => x.Remove(It.Is<OrderEO>(y => y.Id == order.Id)))
              .Verifiable();
            _storeUnitOfWorkMock.Setup(x => x.Save()).Verifiable();
            // Act
            _orderService.DeleteOrder(id);

            // Assert
            _storeUnitOfWorkMock.VerifyAll();
        }
    }
}
