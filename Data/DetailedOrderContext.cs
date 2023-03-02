using Microsoft.EntityFrameworkCore;
using DT191G_projekt.Models;

namespace DT191G_projekt.Data
{
    public class DetailedOrderContext : DbContext
    {
        public DetailedOrderContext(DbContextOptions<DetailedOrderContext> options) : base(options)
        {
        }


        public DbSet<DetailedOrder> DetailedOrder { get; set; }
        public DbSet<Article> Articles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>()
                .HasKey(ar => new { ar.Id });

                
        }
    }
}
