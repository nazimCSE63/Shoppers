using AutoMapper;
using Shoppers.Storage.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreEntity = Shoppers.Storage.Entities.Store;
using ProductEntity = Shoppers.Storage.Entities.Product;
using OrderEntity = Shoppers.Storage.Entities.Order;
namespace Shoppers.Storage
{
    public class StorageProfiles: Profile
    {
        public StorageProfiles()
        {
            CreateMap<StoreEntity, Store>().ReverseMap();
            CreateMap<ProductEntity, Product>().ReverseMap();
            CreateMap<OrderEntity, Order>().ReverseMap();
        }
    }
}
