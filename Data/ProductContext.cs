using DT191G_projekt.Models;
using Microsoft.EntityFrameworkCore;

namespace DT191G_projekt.Data {
    public class ProductContext : DbContext {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options){

            Product = Set<Product>();

        }

        public DbSet<Product> Product {get; set;}

        
    }
}