using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shoppers.Web.Areas.Admin.Models;
using Shoppers.Web.Models;
using Shoppers.Web.Utilities;

namespace Shoppers.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "StoreAdmin")]
    public class StoreController : Controller
    {
        private ILifetimeScope _scope;
        private ILogger<StoreController> _logger;

        public StoreController(ILifetimeScope scope,
            ILogger<StoreController> logger)
        {
            _scope = scope;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult GetStores()
        {
            var dataTableModel = new DataTablesAjaxRequestModel(Request);
            var model = _scope.Resolve<StoreListModel>();
            var data = model.GetStores(dataTableModel);
            return Json(data);
        }
        public IActionResult Create()
        {
            var model = _scope.Resolve<StoreCreateModel>();
            return View(model);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(StoreCreateModel model)
        {
            if (ModelState.IsValid)
            {
                model.Resolve(_scope);
                try
                {
                    model.CreateStore();

                    TempData.Put("Message", new ResponseModel
                    {
                        ResponseMessage = "Store Created Successfully",
                        ResponseType = ResponseTypes.Success
                    });
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    TempData.Put("Message", new ResponseModel
                    {
                        ResponseMessage = "There was a problem in updating quantity.",
                        ResponseType = ResponseTypes.Danger
                    });
                }
            }
            return View();
        }
        public IActionResult Edit(int id)
        {
            var model = _scope.Resolve<StoreEditModel>();
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
        public IActionResult Edit(StoreEditModel model)
        {
            model.Resolve(_scope);
            try
            {
                model.EditStore();
                TempData.Put("Message", new ResponseModel
                {
                    ResponseMessage = "Store Updated Successfully",
                    ResponseType = ResponseTypes.Danger
                });
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                _logger.LogWarning(e, e.Message);
                TempData.Put("Message", new ResponseModel
                {
                    ResponseMessage = e.Message,
                    ResponseType = ResponseTypes.Danger
                });
            }
            return View(model);
        }
        public IActionResult Delete(int id)
        {
            var model = _scope.Resolve<StoreListModel>();
            try
            {
                model.DeleteStore(id);
                TempData.Put("Message", new ResponseModel
                {
                    ResponseMessage = "Store Deleted Successfully",
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
