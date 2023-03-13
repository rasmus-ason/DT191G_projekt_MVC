using DT191G_projekt.Models;
using Microsoft.EntityFrameworkCore;

namespace DT191G_projekt.Data {
    public class AboutUsContext : DbContext {
        public AboutUsContext(DbContextOptions<AboutUsContext> options) : base(options){

            AboutUs = Set<AboutUs>();

        }

        public DbSet<AboutUs> AboutUs {get; set;}

        
    }
}