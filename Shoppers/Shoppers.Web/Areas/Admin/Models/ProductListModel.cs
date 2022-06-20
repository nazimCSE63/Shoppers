using Shoppers.Storage.Services;
using Shoppers.Web.Models;

namespace Shoppers.Web.Areas.Admin.Models
{
    public class ProductListModel
    {
        private readonly IProductService _productService;
        public ProductListModel(IProductService productService)
        {
            _productService = productService;
        }
        public object GetProducts(DataTablesAjaxRequestModel model)
        {
            var data = _productService.GetProducts(model.PageIndex, model.PageSize, model.SearchText, model.GetSortText(new string[] { "Name", "Description" }));
            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                            record.Name,
                            record.ShortDescription,
                            record.Description,
                            record.SKU,
                            record.Size,
                            record.Color,
                            record.Price.ToString(),
                            record?.Image,
                            record.Id.ToString()
                        }
                    ).ToArray()
            };
        }
        public void DeleteProduct(int id)
        {
            _productService.DeleteProduct(id);
        }
    }
}
