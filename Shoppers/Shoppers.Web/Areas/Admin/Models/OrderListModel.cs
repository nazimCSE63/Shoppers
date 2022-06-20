using Autofac;
using AutoMapper;
using Shoppers.Storage.Services;
using Shoppers.Web.Models;

namespace Shoppers.Web.Areas.Admin.Models
{
    public class OrderListModel
    {
        private ILifetimeScope _scope;
        private IOrderService _orderService;

        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Status { get; set; }
        public OrderListModel()
        {
        }
        public OrderListModel(ILifetimeScope scope,
            IOrderService orderService)
        {
            _scope = scope;
            _orderService = orderService;
        }
        public void Resolve(ILifetimeScope scope)
        {
            _orderService = _scope.Resolve<IOrderService>();
        }
        public object GetCategories(DataTablesAjaxRequestModel model)
        {
            var data = _orderService.GetOrders(model.PageIndex, model.PageSize, model.SearchText, model.GetSortText(new string[] { "CustomerName", "ProductName", "Status" }));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                                record.Id.ToString(),
                                record.CustomerId.ToString(),
                                record.CustomerName,
                                record.ProductId.ToString(),
                                record.ProductName,
                                record.Status,
                                record.Id.ToString()
                        }
                    ).ToArray()
            };
        }
        internal void DeleteOrder(int id)
        {
            _orderService.DeleteOrder(id);
        }
    }
}
