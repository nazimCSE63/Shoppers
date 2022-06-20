using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shoppers.Storage.BusinessObjects;
using Shoppers.Storage.Services;
using Shoppers.Web.Areas.Admin.Models;
using Shoppers.Web.Models;

namespace Shoppers.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "StoreAdmin")]
    public class ProductController : Controller
    {
        private ILifetimeScope _scope;
        private ICategoryService _categorService;

        public ProductController(ILifetimeScope scope,
            ICategoryService categoryService)
        {
            _scope = scope;
            _categorService = categoryService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult GetProducts()
        {
            var dataTableModel = new DataTablesAjaxRequestModel(Request);
            var model = _scope.Resolve<ProductListModel>();
            var data = model.GetProducts(dataTableModel);
            return Json(data);
        }
        public IActionResult Create()
        {
            var model = _scope.Resolve<ProductCreateModel>();
            model.Categories = _categorService.GetAllCategory();
            return View(model);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(ProductCreateModel model)
        {
            model.Resolve(_scope);
            model.CreateProduct();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var model = _scope.Resolve<ProductEditModel>();
            model.Load(id);
            return View(model);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(ProductEditModel model)
        {
            model.Resolve(_scope);
            model.EditProduct();
            return View(model);
        }
        public IActionResult Delete(int id)
        {
            var model = _scope.Resolve<ProductListModel>();
            try
            {
                model.DeleteProduct(id);
            }
            catch (InvalidDataException ex)
            {
            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("Index");
        }
    }
}
