
using NUnit.Framework;
using WebTest.AppDBContext;

namespace Service.Test
{
    [TestFixture]
    public class OrderTest
    {
        [SetUp]
        public void Setup(ManagerAppContext manager)
        {
            _managerContext = manager;
        }

        [TearDown]
        public void TearDown()
        {
            _managerContext.DeleteAllData();
        }
        private ManagerAppContext _managerContext;
        
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
