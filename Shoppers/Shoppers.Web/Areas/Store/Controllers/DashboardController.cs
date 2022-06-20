using Microsoft.AspNetCore.Mvc;

namespace Shoppers.Web.Areas.Store.Controllers
{
    public class DashboardController : Controller
    {
        [Area("Store")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
