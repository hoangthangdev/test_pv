using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebTest.AppDBContext;
using WebTest.Models;
using WebTest.Service;
using WebTest.ViewModel;

namespace WebTest.Controllers
{
    [Authorize(Policy = "RequireAdministratorRole")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOrder _order;
        private readonly ManagerAppContext _managerContext;

        public HomeController(ILogger<HomeController> logger, IOrder order, ManagerAppContext manager)
        {
            _logger = logger;
            _order = order;
            _managerContext = manager;
        }
        [AllowAnonymous]
        public IActionResult Index(string textSearch = "", int pageIndex = 1)
        {
            int totalRow;
            var orderData = _order.GetAllOrder(textSearch, pageIndex, out totalRow);
            ViewBag.PageIndex = pageIndex;
            ViewBag.TotalRow = totalRow;
            ViewBag.TextSearch = textSearch;
            return View(orderData);
        }
        
        public async Task<IActionResult> CreateOrder()
        {
            var customers = from cus in _managerContext.Customers select cus;
            var products = from pd in _managerContext.Products select pd;
            var data = new OrderViewModel();
            data.Customers.AddRange(customers.Select(_ => new SelectListItem
            {
                Value = _.Id.ToString(),
                Text = _.Name
            }));
            data.Products.AddRange(products.Select(_ => new SelectListItem
            {
                Value = _.Id.ToString(),
                Text = _.Name
            }));
            return View(data);
        }

        public async Task<IActionResult> AddOrder([FromBody] OrderViewModel order)
        {
            if (ModelState.IsValid)
            {
                var addOrder = await _order.CreateOrder(order);
                if (addOrder)
                    return RedirectToAction(nameof(Index));
            }
            return View(nameof(CreateOrder), order);
        }
    }
}
