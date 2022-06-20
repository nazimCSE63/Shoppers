using AutoMapper;
using Shoppers.Storage.BusinessObjects;
using Shoppers.Web.Areas.Admin.Models;
using Shoppers.Web.Areas.Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreEntity = Shoppers.Storage.Entities.Store;
namespace Shoppers.Storage
{
    public class WebProfiles: Profile
    {
        public WebProfiles()
        {
            CreateMap<CategoryCreateModel, Category>().ReverseMap();
            CreateMap<Category, CategoryEditModel>().ReverseMap();
            CreateMap<Product, ProductDetailsModel>();
        }
    }
}
