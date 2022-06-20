using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Shoppers.Web.Areas.SuperAdmin.Controllers
{
    [Area("SuperAdmin")]
    [Authorize (Roles="SuperAdmin")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
