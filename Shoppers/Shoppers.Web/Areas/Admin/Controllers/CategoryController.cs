using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shoppers.Storage.BusinessObjects;
using Shoppers.Web.Areas.Admin.Models;
using Shoppers.Web.Models;
using Shoppers.Web.Utilities;

namespace Shoppers.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "StoreAdmin")]
    public class CategoryController : Controller
    {
        private ILifetimeScope _scope;
        private ILogger<CategoryController> _logger;

        public CategoryController(ILifetimeScope scope, ILogger<CategoryController> logger)
        {
            _scope = scope;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(CategoryCreateModel model)
        {
            if (ModelState.IsValid)
            {
                model.Resolve(_scope);
                try
                {
                    model.CreateCategory();

                    TempData.Put("Message", new ResponseModel
                    {
                        ResponseMessage = "Category Added Successfully",
                        ResponseType = ResponseTypes.Success
                    });
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex.Message);
                    TempData.Put("Message", new ResponseModel
                    {
                        ResponseMessage = ex.Message,
                        ResponseType = ResponseTypes.Danger
                    });
                }
            }
            return View(model);
        }
        public JsonResult GetCategories()
        {
            var dataTableModel = new DataTablesAjaxRequestModel(Request);
            var model = _scope.Resolve<CategoryListModel>();
            var data = model.GetCategories(dataTableModel);
            return Json(data);
        }
        public IActionResult Edit(int id)
        {
            var model = _scope.Resolve<CategoryEditModel>();
            try
            {
                model.Load(id);
            }
            catch (InvalidOperationException ioe)
            {
                _logger.LogWarning(ioe, ioe.Message);
                TempData.Put("Message", new ResponseModel
                {
                    ResponseMessage = ioe.Message,
                    ResponseType = ResponseTypes.Success
                });
            }
            return View(model);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(CategoryEditModel model)
        {
            model.Resolve(_scope);
            try
            {
                model.EditCategory();

                TempData.Put("Message", new ResponseModel
                {
                    ResponseMessage = "Category Updated Successfully",
                    ResponseType = ResponseTypes.Success
                });
                return RedirectToAction("Index");
            }
            catch (InvalidOperationException ioe)
            {
                _logger.LogWarning(ioe, ioe.Message);
                TempData.Put("Message", new ResponseModel
                {
                    ResponseMessage = ioe.Message,
                    ResponseType = ResponseTypes.Success
                });
            }
            return View(model);
        }
        public ActionResult Delete(int id)
        {
            var model = _scope.Resolve<CategoryListModel>();
            try
            {
                model.DeleteCategory(id);
                TempData.Put("Message", new ResponseModel
                {
                    ResponseMessage = "Category Deleted Successfully",
                    ResponseType = ResponseTypes.Success
                });
            }
            catch (Exception ex)
            {
                TempData.Put("Message", new ResponseModel
                {
                    ResponseMessage = ex.Message,
                    ResponseType = ResponseTypes.Danger
                });
            }
            return RedirectToAction("Index");
        }

    }
}
