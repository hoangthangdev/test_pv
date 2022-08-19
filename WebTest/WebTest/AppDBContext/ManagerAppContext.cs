using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebTest.Models;
using WebTest.ViewModel;

namespace WebTest.AppDBContext
{
    public class ManagerAppContext : DbContext
    {
        public ManagerAppContext(DbContextOptions<ManagerAppContext> options) : base(options)
        {
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
