using Shoppers.Storage.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppers.Storage.Services
{
    public interface ICategoryService
    {
        void CreateCategory(Category category);
        IList<Category> GetAllCategory();
        Category GetCategory(int id);
        void EditCategory(Category category);
        void DeleteCategory(int id);
        (int total, int totalDisplay, IList<Category> records) GetCategories(int pageIndex, int pageSize,
     string searchText, string orderBy);
    }
}
