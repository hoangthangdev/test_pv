using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using WebTest.AppDBContext;
using WebTest.Models;
using WebTest.Service;

namespace BasicTest
{
    [TestFixture]
    public class OrderTest
    {
        private static DbContextOptions<ManagerAppContext> dbContextOptions =
            new DbContextOptionsBuilder<ManagerAppContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
        [OneTimeSetUp]
        public void SetUp()
        {
            _managerContext = new ManagerAppContext(dbContextOptions);
            _managerContext.Database.EnsureCreated();
            var mock = new Mock<ILogger<OrderService>>();
            ILogger<OrderService> logger = mock.Object;
            logger = Mock.Of<ILogger<OrderService>>();
            _orderService = new OrderService(logger, _managerContext);
            SeeDatabase();
        }
        private ManagerAppContext _managerContext;
        private IOrder _orderService;

        [OneTimeTearDown]
        public void CleanUp()
        {
            _managerContext.Database.EnsureDeleted();
        }

        private void SeeDatabase()
        {
            var customers = new List<Customer>
            {
                new Customer
                {
                    Address = "123/1",
                    Id = 1,
                    Name = "khach 1",
                },
                new Customer
                {
                    Address = "123/2",
                    Id = 2,
                    Name = "khach 2",
                }
            };
            _managerContext.Customers.AddRange(customers);
            var orders = new List<Order>
            {
                new Order
                {
                    Id = 1,
                    CustomerId = 1,
                    ProductId = 1,
                    Amount = 10,
                    OrderDate = DateTime.Now
                },
                new Order
                {
                    Id = 2,
                    CustomerId = 1,
                    ProductId = 2,
                    Amount = 10,
                    OrderDate = DateTime.Now
                },
                new Order
                {
                    Id = 3,
                    CustomerId = 2,
                    ProductId = 3,
                    Amount = 10,
                    OrderDate = DateTime.Now
                },
                new Order
                {
                    Id = 4,
                    CustomerId = 2,
                    ProductId = 1,
                    Amount = 10,
                    OrderDate = DateTime.Now
                },
            };
            _managerContext.Orders.AddRange(orders);

            var products = new List<Product>
            {
                new Product
                {
                    Id = 1,
                    CategoryId = 1,
                    Name = "product 1",
                    Description = "Description product",
                    Quantity = 10,
                    Price = 10
                },
                new Product
                {
                    Id = 2,
                    CategoryId = 1,
                    Name = "product 2",
                    Description = "Description product",
                    Quantity = 10,
                    Price = 10
                },
                new Product
                {
                    Id = 3,
                    CategoryId = 2,
                    Name = "product 3",
                    Description = "Description product",
                    Quantity = 10,
                    Price = 10
                }
            };

            _managerContext.Products.AddRange(products);

            var cates = new List<Category>
            {
                new Category
                {
                    Id = 1,
                    Description = "Description cate",
                    Name = "Name 1"
                },
                new Category
                {
                    Id = 2,
                    Description = "Description cate",
                    Name = "Name 2"
                },
            };
            _managerContext.Categories.AddRange(cates);

            _managerContext.SaveChanges();
        }

        [TestCase("", 1, 0, TestName = "GetAllOrderTestCase - all data")]
        [TestCase("Name 1", 1, 1, TestName = "GetAllOrderTestCase - a data")]
        [TestCase("khachch 1", 1, 2, TestName = "GetAllOrderTestCase - null")]
        [TestCase("", 2, 3, TestName = "GetAllOrderTestCase - null 2")]
        public void GetAllOrderTestCase(string textSearch, int pageIndex, int caseNo)
        {
            var totalRow = 0;
            var result = _orderService.GetAllOrder(textSearch, pageIndex, out totalRow);
            switch (caseNo)
            {
                case 0:
                    totalRow.Is(4);
                    result.Count.Is(4);
                    break;
                case 1:
                    totalRow.Is(3);
                    result.Count.Is(3);
                    break;
                case 2:
                    totalRow.Is(0);
                    result.Count.Is(0);
                    break;
                case 3:
                    totalRow.Is(4);
                    result.Count.Is(0);
                    break;
            }
        }
    }
}
