using AutoMapper;
using Shoppers.Storage.BusinessObjects;
using Shoppers.Web.Areas.Admin.Models;

namespace Shoppers.Web.Areas.Admin.Profiles
{
    public class WebProfile : Profile
    {
        public WebProfile()
        {
            CreateMap<OrderEditModel, Order>().ReverseMap();
        }
    }
}
