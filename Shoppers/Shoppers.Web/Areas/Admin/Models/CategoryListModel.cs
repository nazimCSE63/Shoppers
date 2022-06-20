using Autofac;
using AutoMapper;
using Shoppers.Storage.Services;
using Shoppers.Web.Models;

namespace Shoppers.Web.Areas.Admin.Models
{
    public class CategoryListModel
    {
        private ICategoryService _categoryService;
        private IMapper _mapper;
        //private ILifetimeScope _scope;

        public int Id { get; set; } 
        public string Name { get; set; }
        public CategoryListModel()
        {

        }
        public CategoryListModel(ICategoryService categoryService,
            IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }
        public void Resolve(ILifetimeScope scope)
        {
            //_categoryService = _scope.Resolve<ICategoryService>();
            //_mapper = _scope.Resolve<IMapper>();
        }

        public object GetCategories(DataTablesAjaxRequestModel model)
        {
            var data = _categoryService.GetCategories(model.PageIndex, model.PageSize, model.SearchText, model.GetSortText(new string[] { "Name", "Description" }));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                                record.Name,
                                record.Description,
                                record.Id.ToString()
                        }
                    ).ToArray()
            };
        }

        internal void DeleteCategory(int id)
        {
            _categoryService.DeleteCategory(id);
        }
    }
}
