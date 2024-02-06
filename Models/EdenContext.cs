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
    }
}
