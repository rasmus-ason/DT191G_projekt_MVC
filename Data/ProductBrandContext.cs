using DT191G_projekt.Models;
using Microsoft.EntityFrameworkCore;

namespace DT191G_projekt.Data {
    public class ProductBrandContext : DbContext {
        public ProductBrandContext(DbContextOptions<ProductBrandContext> options) : base(options){

            ProductBrand = Set<ProductBrand>();

        }

        public DbSet<ProductBrand> ProductBrand {get; set;}

        
    }
}