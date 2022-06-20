using Shoppers.Storage.Services;
using Shoppers.Web.Models;

namespace Shoppers.Web.Areas.Admin.Models
{
    public class InventoryListModel
    {
        private readonly IInventoryService _inventoryService;
        public InventoryListModel(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        public object GetPagedInventoryList(DataTablesAjaxRequestModel model)
        {
            var data = _inventoryService.GetInventoryList(
                model.PageIndex, 
                model.PageSize, 
                model.SearchText, 
                model.GetSortText(new string[] { "Name", "SKU", "Quantity" }));
            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                            record.Name,
                            record.SKU,
                            record.Quantity.ToString(),
                            record.Id.ToString()
                        }
                    ).ToArray()
            };
        }
    }
}
