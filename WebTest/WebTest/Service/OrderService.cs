using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.Language.Intermediate;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebTest.AppDBContext;
using WebTest.Models;
using WebTest.ViewModel;

namespace WebTest.Service
{
    public class OrderService : IOrder
    {
        private readonly ManagerAppContext _managerContext;
        private readonly ILogger<OrderService> _logger;
        public OrderService(ILogger<OrderService> logger, ManagerAppContext manager)
        {
            _managerContext = manager;
            _logger = logger;
        }
        public List<OrderOutDto> GetAllOrder(string textSearch, int pageIndex, out int totalRow)
        {
            var orders = from od in _managerContext.Orders
                         join cus in _managerContext.Customers on od.CustomerId equals cus.Id
                         join pd in _managerContext.Products on od.ProductId equals pd.Id
                         join ct in _managerContext.Categories on pd.CategoryId equals ct.Id
                         into ctTemp
                         from lct in ctTemp.DefaultIfEmpty()
                         where string.IsNullOrEmpty(textSearch) || lct.Name.Contains(textSearch)
                         select new OrderOutDto
                         {
                             ProductName = pd.Name,
                             CustomerName = cus.Name,
                             CategoryName = lct.Name,
                             OrderDate = od.OrderDate,
                             Amount = pd.Price
                         };
            var dt = orders.Skip((pageIndex - 1) * 10).Take(10).ToList();
            totalRow = orders.ToList().Count;
            return dt;
        }

        public async Task<bool> CreateOrder(OrderViewModel order)
        {
            try
            {
                if (order == null)
                    return false;
                if (order.Id > 0)
                    _managerContext.Update(order);
                else
                    _managerContext.Add(order);
                _ = _managerContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e,"Create order err");
                throw;
            }
            return true;
        }
    }
}
