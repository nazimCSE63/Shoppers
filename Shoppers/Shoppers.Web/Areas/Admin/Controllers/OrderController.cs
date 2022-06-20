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
    public class OrderController : Controller
    {
        private ILifetimeScope _scope;
        private ILogger<OrderController> _logger;

        public OrderController(ILifetimeScope scope, ILogger<OrderController> logger)
        {
            _scope = scope;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult GetOrders()
        {
            var dataTableModel = new DataTablesAjaxRequestModel(Request);
            var model = _scope.Resolve<OrderListModel>();
            var data = model.GetCategories(dataTableModel);
            return Json(data);
        }
        public IActionResult Edit(int id)
        {
            var model = _scope.Resolve<OrderEditModel>();
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
        public IActionResult Edit(OrderEditModel model)
        {
            model.Resolve(_scope);
            try
            {
                model.EditOrder();

                TempData.Put("Message", new ResponseModel
                {
                    ResponseMessage = "Order Approved Successfully",
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

        [ValidateAntiForgeryToken, HttpPost]
        public ActionResult Delete(int id)
        {
            var model = _scope.Resolve<OrderListModel>();
            try
            {
                model.DeleteOrder(id);
                TempData.Put("Message", new ResponseModel
                {
                    ResponseMessage = "Order Deleted Successfully",
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
