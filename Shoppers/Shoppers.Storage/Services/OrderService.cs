using AutoMapper;
using Shoppers.Storage.BusinessObjects;
using Shoppers.Storage.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderEntity = Shoppers.Storage.Entities.Order;

namespace Shoppers.Storage.Services
{
    public class OrderService : IOrderService
    {
        private readonly IStoreUnitOfWork _storeUnitOfWork;
        private readonly IMapper _mapper;

        public OrderService(IStoreUnitOfWork storeUnitOfWork, IMapper mapper)
        {
            _storeUnitOfWork = storeUnitOfWork;
            _mapper = mapper;
        }

        public (int total, int totalDisplay, IList<Order> records) GetOrders(int pageIndex, int pageSize, string searchText, string orderBy)
        {
            var result = _storeUnitOfWork.Orders.GetDynamic(x => x.Status.Contains(searchText),
              orderBy, string.Empty, pageIndex, pageSize, true);

            List<Order> orders = new List<Order>();
            foreach (OrderEntity order in result.data)
            {
                orders.Add(_mapper.Map<Order>(order));
            }
            return (result.total, result.totalDisplay, orders);
        }
        public void DeleteOrder(int id)
        {
            _storeUnitOfWork.Orders.Remove(id);
            _storeUnitOfWork.Save();
        }

        public Order GetOrder(int id)
        {
            var orderEntity = _storeUnitOfWork.Orders.GetById(id);
            var order = _mapper.Map<Order>(orderEntity);

            return order;
        }
        public void EditOrder(Order order)
        {
            var orderEntity = _storeUnitOfWork.Orders.GetById(order.Id);
            orderEntity = _mapper.Map(order, orderEntity);
            _storeUnitOfWork.Save();
        }
    }
}
