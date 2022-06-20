using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shoppers.Storage.Entities;
using Shoppers.Web.Services;

namespace Shoppers.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "StoreAdmin")]
    public class CustomersController : Controller
    {
        private CustomerService _CustomerService;

        public CustomersController(CustomerDbContext context)
        {
            _CustomerService = new CustomerService(context);
        }

        // GET: Admin/Customers
        public async Task<IActionResult> Index()
        {
            return View(_CustomerService.GetAll());
        }

    }
}
