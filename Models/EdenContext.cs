using eden_food.Models;
using Microsoft.EntityFrameworkCore;

namespace eden_western_food.Models
{
    public class EdenContext : DbContext
    {
        public DbSet<Category> Category { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<MenuItem> MenuItem { get; set; }  
        public DbSet<Customer> Customer { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite(@"Data Source=./eden_db.db");
        }

        // Manually adding things in since EF doesn't know how to deal with these 
        // relationships...

        /*
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderItem>()
                .HasOne(o => o.MenuCodeNavigation)
                .WithMany()
                .HasForeignKey(o => o.MenuCode);

            // Also do the same thing for the MenuItem class:
            modelBuilder.Entity<MenuItem>()
                .HasOne(o => o.CatCodeNavigation)
                .WithMany()
                .HasForeignKey(o => o.CatCode);

            base.OnModelCreating(modelBuilder);
        }      
        */
    }
}
