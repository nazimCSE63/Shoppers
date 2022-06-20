using Autofac;
using AutoMapper;
using Shoppers.Storage.BusinessObjects;
using Shoppers.Storage.Services;
using ProductBO = Shoppers.Storage.BusinessObjects.Product;

namespace Shoppers.Web.Areas.Admin.Models
{
    public class InventoryUpdateModel
    {
        private IInventoryService _inventoryService;
        private IMapper _mapper;
        private ILifetimeScope _scope;

        public int Id { get; set; }
        public int Quantity { get; set; }
        public InventoryUpdateModel()
        {

        }

        public InventoryUpdateModel(IMapper mapper, 
            IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
            _mapper = mapper;
        }
        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _inventoryService = _scope.Resolve<IInventoryService>();
            _mapper = _scope.Resolve<IMapper>();
        }
        public int Load(int id)
        {
            var data = _inventoryService.GetQuantity(id);
           
            return data.Quantity;
        }
        public void AddQuantity(int id)
        {
            var data = _inventoryService.GetQuantity(id);
            Quantity = data.Quantity+Quantity;
            var product = new ProductBO
            {
                Id = Id,
                Quantity = Quantity,
            };

            _inventoryService.UpdateQuantity(product);
        }

        public void DeleteQuantity(int id)
        {
            var data = _inventoryService.GetQuantity(id);
            Quantity = data.Quantity - Quantity;

            var product = new ProductBO
            {
                Id = Id,
                Quantity = Quantity,
            };

            _inventoryService.UpdateQuantity(product);
        }
    }
}
