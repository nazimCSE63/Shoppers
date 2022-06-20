using AutoMapper;
using Shoppers.Storage.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CategoryEntity = Shoppers.Storage.Entities.Category;
namespace Shoppers.Storage
{
    public class StorageProfile: Profile
    {
        public StorageProfile()
        {
            CreateMap<Category, CategoryEntity>().ReverseMap();
        }
    }
}
