using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Cosmos;

namespace hw4_InternetStore_Azure.Models
{
    public class AppContextt:DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToContainer("Products");
            modelBuilder.Entity<Category>().ToContainer("Categories");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseCosmos(
              "AccountEndpoint=https://oliinykstore.documents.azure.com:443/;" +
              "AccountKey=7PGmhxtUiLnl9fr4h3lvWod3Lk3Jj0CynvfbDWzMfF8YlgL3MmEj4YbMBlrG8wlmCCaDMQPxYBgGACDbcArwHQ==;",
              "MyInternrtStore");
        }


    }

   
}
