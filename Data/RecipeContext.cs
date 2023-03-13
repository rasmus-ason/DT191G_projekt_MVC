using Microsoft.EntityFrameworkCore;
using DT191G_projekt.Models;

namespace DT191G_projekt.Data
{
    public class RecipeContext : DbContext
    {
        public RecipeContext(DbContextOptions<RecipeContext> options) : base(options)
        {
            
        }


        public DbSet<Recipe> Recipe { get; set; }
        public DbSet<Ingredient> Ingredient { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ingredient>()
                .HasKey(ing => new { ing.Id });

                
        }
    }
}
