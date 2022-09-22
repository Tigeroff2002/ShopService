using Microsoft.EntityFrameworkCore;
using ShopService.Models;

namespace ShopService.Data
{
    public class RetailStoreDataContext : DbContext
    {
        public RetailStoreDataContext(DbContextOptions<RetailStoreDataContext> options)
            : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producer>()
                .HasData(
                    new Producer
                    {
                        Id = 1,
                        Name = "Samsung",
                        Country = "Вьетнам",
                        Popularity = 4.9f,
                        WebSite = "www.samsung.com/ru"
                    }
                );
        }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<DeviceType> DeviceTypes { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Trading> Trades { get; set; }
    }
}
