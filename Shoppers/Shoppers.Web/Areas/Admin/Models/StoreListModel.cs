using Shoppers.Storage.Services;
using Shoppers.Web.Models;

namespace Shoppers.Web.Areas.Admin.Models
{
    public class StoreListModel
    {
        private readonly IStoreService _storeService;
        public StoreListModel(IStoreService storeService)
        {
            _storeService = storeService;
        }
        public object GetStores(DataTablesAjaxRequestModel model)
        {
            var data = _storeService.GetStores(model.PageIndex, model.PageSize, model.SearchText, model.GetSortText(new string[] { "Name", "Fee" }));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                                record.Name,
                                record.Banner,
                                record.Logo,
                                record.Id.ToString()
                        }
                    ).ToArray()
            };
        }

        public void DeleteStore(int id)
        {
            _storeService.DeleteStore(id);
        }
    }
}
