using DT191G_projekt.Models;
using Microsoft.EntityFrameworkCore;

namespace DT191G_projekt.Data {
    public class CustomerOrderContext : DbContext {
        public CustomerOrderContext(DbContextOptions<CustomerOrderContext> options) : base(options){

            CustomerOrder = Set<CustomerOrder>();

        }

        public DbSet<CustomerOrder> CustomerOrder {get; set;}

        
    }
}