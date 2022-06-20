using Autofac;
using AutoMapper;
using Shoppers.Storage.BusinessObjects;
using Shoppers.Storage.Services;

namespace Shoppers.Web.Areas.Admin.Models
{
    public class OrderEditModel
    {
        private ILifetimeScope _scope;
        private IOrderService _orderService;
        private IMapper _mapper;
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Status { get; set; }
        public OrderEditModel()
        {
        }
        public OrderEditModel(ILifetimeScope scope,
            IOrderService orderService,
            IMapper mapper)
        {
            _scope = scope;
            _orderService = orderService;
            _mapper = mapper;
        }
        public void Resolve(ILifetimeScope scope)
        {
            _orderService = scope.Resolve<IOrderService>();
            _mapper = scope.Resolve<IMapper>();
        }
        public void Load(int id)
        {
            var order = _orderService.GetOrder(id);
            _mapper.Map(order, this);
        }
        public void EditOrder()
        {
            var order = _mapper.Map<Order>(this);
            _orderService.EditOrder(order);
        }
   
    }
}
