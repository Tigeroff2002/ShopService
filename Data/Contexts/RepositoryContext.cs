using Data.Contexts.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;

namespace Data.Contexts
{
    public sealed class RepositoryContext
        : IRepositoryContext
    {
        public RepositoryContext(
            ILogger<RepositoryContext> logger,
            RetailStoreDataContext context)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _storeContext = context ?? throw new ArgumentNullException(nameof(context));
        }
        public DbSet<Product> Products => _storeContext.Products;
        public DbSet<User> Clients => _storeContext.Clients;
        public DbSet<Order> Orders => _storeContext.Orders;
        public DbSet<Warehouse> Warehouses => _storeContext.Warehouses;
        public DbSet<Shop> Shops => _storeContext.Shops;
        public DbSet<SummUpProduct> SummUpProducts => _storeContext.SummUpProducts;
        public DbSet<Basket> Baskets => _storeContext.Baskets;

        public void SaveChanges()
        {
            _logger.LogDebug("Save changes");

            _storeContext.SaveChanges();

            _logger.LogDebug("Changes sent to database");
        }

        private readonly ILogger<RepositoryContext> _logger;
        private readonly RetailStoreDataContext _storeContext;
    }
}
