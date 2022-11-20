using Microsoft.EntityFrameworkCore;
using Models;

namespace Data.Contexts
{
    public class RetailStoreDataContext : DbContext
    {
        public RetailStoreDataContext(DbContextOptions<RetailStoreDataContext> options)
            : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        public DbSet<Producer> Producers { get; set; }
        public DbSet<DeviceType> DeviceTypes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Clients { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Shipping> Shippings { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<SummUpProduct> SummUpProducts { get; set; }
    }
}
