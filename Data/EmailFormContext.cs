using DT191G_projekt.Models;
using Microsoft.EntityFrameworkCore;

namespace DT191G_projekt.Data {
    public class EmailFormContext : DbContext {
        public EmailFormContext(DbContextOptions<EmailFormContext> options) : base(options){

            Email = Set<EmailForm>();

        }

        public DbSet<EmailForm> Email {get; set;}

        
    }
}