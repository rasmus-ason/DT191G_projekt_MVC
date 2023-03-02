using DT191G_projekt.Models;
using Microsoft.EntityFrameworkCore;

namespace DT191G_projekt.Data {
    public class ProductCategoryContext : DbContext {
        public ProductCategoryContext(DbContextOptions<ProductCategoryContext> options) : base(options){

            ProductCategory = Set<ProductCategory>();

        }

        public DbSet<ProductCategory> ProductCategory {get; set;}

        
    }
}