using Microsoft.EntityFrameworkCore;
using ShopService.Models;
namespace ShopService.Data
{
    public class RetailStoreProfileContext : DbContext
    {
        public RetailStoreProfileContext(DbContextOptions<RetailStoreProfileContext> options)
            : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Option> Options { get; set; }
    }
}
