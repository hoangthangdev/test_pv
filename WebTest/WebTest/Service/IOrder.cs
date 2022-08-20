using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebTest.Models;
using WebTest.ViewModel;

namespace WebTest.Service
{
    public interface IOrder
    {
        List<OrderOutDto> GetAllOrder(string textSearch,int pageIndex, out int totalRow);

        Task<bool> CreateOrder(OrderViewModel order);
    }
}
