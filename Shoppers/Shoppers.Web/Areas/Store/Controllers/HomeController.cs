using Autofac;
using Microsoft.AspNetCore.Mvc;
using Shoppers.Web.Areas.Store.Models;

namespace Shoppers.Web.Areas.Store.Controllers
{
    [Area("Store")]
    public class HomeController : Controller
    {
        private ILifetimeScope _scope;
        private ILogger<HomeController> _logger;

        public HomeController(ILifetimeScope scope,
            ILogger<HomeController> logger)
        {
            _scope = scope;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var model = _scope.Resolve<FrontProductListModel>();
            model.LoadProducts();
            return View(model);
        }
        public IActionResult ProductDetails(int id)
        {
            var model = _scope.Resolve<ProductDetailsModel>();
            model.Load(id);
            return View(model);
        }
        public IActionResult CheckOut(CheckoutModel model)
        {
            /*var model = _scope.Resolve<ProductDetailsModel>();
            model.Load(id);*/
            return View();
        }
        public IActionResult SignUp()
        {
            return View();
        }
     
    }
}
