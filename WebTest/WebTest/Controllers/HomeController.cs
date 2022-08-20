using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebTest.AppDBContext;
using WebTest.Service;
using WebTest.ViewModel;

namespace WebTest.Controllers
{
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

        public IActionResult Index(string textSearch = "", int? pageIndex = 1)
        {
            pageIndex = pageIndex ?? 1;
            int totalRow;
            var orderData = _order.GetAllOrder(textSearch, (int)pageIndex, out totalRow);
            ViewBag.PageIndex = pageIndex;
            ViewBag.TotalRow = totalRow;
            ViewBag.TextSearch = textSearch;
            _logger.LogInformation("index ok");
            return View(orderData);
        }

        public async Task<IActionResult> CreateOrder()
        {
            var customers = _managerContext.Customers.ToList();
            var products = _managerContext.Products.ToList();
            var data = new OrderViewModel();
            if (customers.Count > 0)
                data.Customers = customers.Select(_ => new SelectListItem
                {
                    Value = _.Id.ToString(),
                    Text = _.Name 
                }).ToList();
           
            if (products.Count > 0) 
            {
                data.Products = products.Select(_ => new SelectListItem
                {
                    Value = _.Id.ToString(),
                    Text = _.Name
                }).ToList();
            }
            return View(data);
        }

        public async Task<IActionResult> AddOrder(OrderViewModel order)
        {
            if (ModelState.IsValid)
            {
                var addOrder = await _order.CreateOrder(order);
                if (addOrder)
                    return RedirectToAction(nameof(Index),"",1);
            }
            return View(nameof(CreateOrder), order);
        }
    }
}
