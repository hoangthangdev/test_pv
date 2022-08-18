using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebTest.Models;
using WebTest.ViewModel;

namespace WebTest.AppDBContext
{
    public class ManagerContext : DbContext
    {

        public ManagerContext(DbContextOptions<ManagerContext> options) : base(options)
        {
            // Phương thức khởi tạo này chứa options để kết nối đến MS SQL Server
            // Thực hiện điều này khi Inject trong dịch vụ hệ thống
        }
        public DbSet<Customer> Customers { set; get; }    
        public DbSet<Order> Orders { set; get; }  
        public DbSet<Product> Products { set; get; }  
        public DbSet<Category> Categories { set; get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }


        public void DeleteAllData()
        {
            foreach (var entity in Orders)
            {
                Orders.Remove(entity);
            }
        }
    }
}
