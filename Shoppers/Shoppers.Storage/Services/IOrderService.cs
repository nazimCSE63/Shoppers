using Shoppers.Storage.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppers.Storage.Services
{
    public interface IOrderService
    {
        (int total, int totalDisplay, IList<Order> records) GetOrders(int pageIndex, int pageSize,
    string searchText, string orderBy);

        void DeleteOrder(int id);
        Order GetOrder(int id);
        void EditOrder(Order order);
    }
}
