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
    public class InventoryController : Controller
    {
        private ILifetimeScope _scope;
        private readonly ILogger<InventoryController> _logger;
        public InventoryController(ILogger<InventoryController> logger, ILifetimeScope scope)
        {
            _logger = logger;
            _scope = scope;           
        }
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult GetInventoryList()
        {
            var dataTableModel = new DataTablesAjaxRequestModel(Request);
            var model = _scope.Resolve<InventoryListModel>();
            var data = model.GetPagedInventoryList(dataTableModel);
            return Json(data);
        }
        public IActionResult add(int id)
        {
            var model = _scope.Resolve<InventoryUpdateModel>();
            //model.Load(id);
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult add(InventoryUpdateModel model)
        { 
            if (ModelState.IsValid)
            {
                model.Resolve(_scope);

                try
                {
                    model.AddQuantity(model.Id);

                    TempData.Put("Message", new ResponseModel
                    {
                        ResponseMessage = "product added Successfully",
                        ResponseType = ResponseTypes.Success
                    });

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                    TempData.Put("Message", new ResponseModel
                    {
                        ResponseMessage = "There was a problem in updating quantity.",
                        ResponseType = ResponseTypes.Danger
                    });
                }
            }
            return View(model);
        }

        public IActionResult delete(int id)
        {
            var model = _scope.Resolve<InventoryUpdateModel>();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult delete(InventoryUpdateModel model)
        { 
            if (ModelState.IsValid)
            {
                model.Resolve(_scope);
                var preQuantity = model.Load(model.Id);
                try
                {
                    if (model.Quantity <= preQuantity)
                    {
                        model.DeleteQuantity(model.Id);

                        TempData.Put("Message", new ResponseModel
                        {
                            ResponseMessage = "product reduced Successfully",
                            ResponseType = ResponseTypes.Danger
                        });

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData.Put("Message", new ResponseModel
                        {
                            ResponseMessage = "You cannot delete more than the available products",
                            ResponseType = ResponseTypes.Warning
                        });

                        return RedirectToAction("Index");
                    }
                       
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                    TempData.Put("Message", new ResponseModel
                    {
                        ResponseMessage = "There was a problem in updating quantity.",
                        ResponseType = ResponseTypes.Danger
                    });
                }
            }
            return View(model);
        }
    }
    
}
