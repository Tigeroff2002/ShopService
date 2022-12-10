using Data.Repositories.Abstractions;
using Data.Contexts;
using Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Data.Repositories
{
    public sealed class SummUpProductsRepository
        : ISummUpProductsRepository
    {
        public SummUpProductsRepository(
            ILogger<SummUpProductsRepository> logger,
            RetailStoreDataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _logger.LogInformation("ProductsRepository was created just now");
        }

        public async Task Add(SummUpProduct summUpProduct, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(summUpProduct);

            token.ThrowIfCancellationRequested();

            _ = await _context.AddAsync(summUpProduct, token);  
        }

        public void Delete(SummUpProduct summUpProduct, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(summUpProduct);

            token.ThrowIfCancellationRequested();

            _ = _context.Remove(summUpProduct);
        }

        public async Task<bool> Find(SummUpProduct summUpProduct, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(summUpProduct);

            token.ThrowIfCancellationRequested();

            var findedProductGroup = await _context.SummUpProducts.FindAsync(summUpProduct, token);

            return findedProductGroup == null ? false : true;
        }

        public IList<SummUpProduct> GetAllProductsGroupsFromBasket(Basket basket, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(basket);

            token.ThrowIfCancellationRequested();

            return _context.Baskets
                .FirstOrDefault(b => b.Equals(basket))!
                .SummUpProducts!
                .ToList();
        }

        public IList<SummUpProduct> GetAllProductsGroupsFromOrder(Order order, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(order);

            token.ThrowIfCancellationRequested();

            return _context.Orders
                .FirstOrDefault(o => o.Equals(order))!
                .SummUpProducts!
                .ToList();
        }

        public IList<SummUpProduct> GetAllProductsGroupsFromShop(Shop shop, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(shop);

            token.ThrowIfCancellationRequested();

            return _context.Shops
                .FirstOrDefault(s => s.Equals(shop))!
                .ProductQuantities!
                .ToList();
        }

        public IList<SummUpProduct> GetAllProductsGroupsFromWarehouse(Warehouse warehouse, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(warehouse);

            token.ThrowIfCancellationRequested();

            return _context.Warehouses
                .FirstOrDefault(w => w.Equals(warehouse))!
                .ProductQuantities!
                .ToList();
        }

        public async Task SaveChangesAsync(CancellationToken token)
        {
            _logger.LogInformation("ClientsRepository called saving data to DB");

            token.ThrowIfCancellationRequested();

            _ = await _context.SaveChangesAsync(token);
        }

        public void Update(SummUpProduct summUpProduct, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(summUpProduct);

            token.ThrowIfCancellationRequested();

            _context.Entry(summUpProduct).State = EntityState.Modified;
        }

        private readonly ILogger<SummUpProductsRepository> _logger;
        private readonly RetailStoreDataContext _context;
    }
}
