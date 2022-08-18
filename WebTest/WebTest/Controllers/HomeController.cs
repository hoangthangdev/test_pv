using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using WebTest.Models;
using WebTest.Service;
using WebTest.ViewModel;

namespace WebTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOrder _order;

        public HomeController(ILogger<HomeController> logger, IOrder order)
        {
            _logger = logger;
            _order = order;
        }

        public IActionResult Index(string textSearch, int pageIndex = 1)
        {
            int totalRow;
            var orderData = _order.GetAllOrder(textSearch, pageIndex, out totalRow);
            ViewBag.PageIndex = pageIndex;
            ViewBag.TotalRow = totalRow;
            return View(orderData);
        }


        public async Task<IActionResult> CreateOrder([FromBody] OrderViewModel order)
        {
            if (ModelState.IsValid)
            {
                var addOrder = await _order.CreateOrder(order);
                if (addOrder)
                    return RedirectToAction(nameof(Index));
            }
            return View();
        }
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (WindowsIdentity.GetAnonymous().IsAnonymous)
                throw new NotAllowedUserRoleException(urlReferer: "_greenfileConfig.BaseUrl");
            await next();
        }
        public class NotAllowedUserRoleException : Exception
        {
            public string UrlReferer { get; }

            public NotAllowedUserRoleException(string urlReferer, string message = "") : base(message)
            {
                UrlReferer = urlReferer;
            }
        }
    }
}
