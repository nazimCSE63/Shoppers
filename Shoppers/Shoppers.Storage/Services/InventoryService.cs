using AutoMapper;
using Shoppers.Storage.BusinessObjects;
using Shoppers.Storage.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductEntity = Shoppers.Storage.Entities.Product;


namespace Shoppers.Storage.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IStoreUnitOfWork _inventoryUnitOfWork;
        private readonly IMapper _mapper;
        public InventoryService(IStoreUnitOfWork inventoryUnitOfWork, IMapper mapper)
        {
            _inventoryUnitOfWork = inventoryUnitOfWork;
            _mapper = mapper;
        }
        public (int total, int totalDisplay, IList<Product> records) GetInventoryList(int pageIndex, 
            int pageSize, string searchText, string orderBy)
        {

            var result = _inventoryUnitOfWork.Products.GetDynamic(x => x.Name.Contains(searchText),
                orderBy, string.Empty, pageIndex, pageSize, true);

            List<Product> products = new List<Product>();
            foreach (ProductEntity product in result.data)
            {
                products.Add(_mapper.Map<Product>(product));
            }

            return (result.total, result.totalDisplay, products);
        }

        public IList<Product> GetInventoryList()
        {
            throw new NotImplementedException();
        }

        public Product GetQuantity(int id)
        {
            var productEntity = _inventoryUnitOfWork.Products.GetById(id);

            var product = _mapper.Map<Product>(productEntity);

            return product;
        }

        public void UpdateQuantity(Product product)
        {
            var productEntity = _inventoryUnitOfWork.Products.GetById(product.Id);
            productEntity.Quantity = product.Quantity;
            _inventoryUnitOfWork.Save();
        }
    }
}
