using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using NUnit.Framework;

namespace Service.Test
{
    [TestFixture]
    public class OrderTest
    {
        [SetUp]
        public void Setup()
        {
            _dbContext = TestDatabaseFactory.CreateCustomGreenfileDbContextMemory();
            var logger = new LoggerFactory().CreateLogger<TransactionBlock>();
            var serviceLogger = new LoggerFactory().CreateLogger<GF12Service>();
            var date = new DateGenerator(new SystemDate());
            var mockPermissionService = new Mock<IPermissionService>();
            _gf12Service = new GF12Service(date, _dbContext, new TransactionBlock(logger), serviceLogger,
                mockPermissionService.Object);
            _dateTime = DateTime.Now.AddHours(-1);
        }

        private ManagerCotext
        [TestCase]
        public void GetAllOrderTest_Exit()
        {
            
        }
        [TestCase]
        public void GetAllOrderTest_NotExit()
        {
            
        }
    }
}
