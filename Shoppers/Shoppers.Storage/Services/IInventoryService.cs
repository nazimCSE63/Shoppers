using Shoppers.Storage.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppers.Storage.Services
{
    public interface IInventoryService
    {
        (int total, int totalDisplay, IList<Product> records) GetInventoryList(int pageIndex, int pageSize,
            string searchText, string orderBy);
        void UpdateQuantity(Product product);
        Product GetQuantity(int id);
        IList<Product> GetInventoryList();
    }
}
