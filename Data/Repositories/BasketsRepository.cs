using Data.Repositories.Abstractions;
using Data.Contexts;
using Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public sealed class BasketsRepository
        : IBasketsRepository
    {
        public BasketsRepository(
            ILogger<BasketsRepository> logger, 
            RetailStoreDataContext context)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _context = context ?? throw new ArgumentNullException(nameof(context));

            _logger.LogInformation("BasketsRepository has created just now");
        }

        public void AddProductGroup(User user, SummUpProduct summUpProduct, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(user);

            ArgumentNullException.ThrowIfNull(summUpProduct);

            token.ThrowIfCancellationRequested();

            _context.Baskets!
                .FirstOrDefault(basket => basket == user.Basket)!
                .SummUpProducts!
                .Add(summUpProduct);

            _context.Clients!
                .FirstOrDefault(u => u.Equals(user))!
                .Basket!
                .SummUpProducts!
                .Add(summUpProduct);
        }

        public void ClearBasket(User user, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(user);

            token.ThrowIfCancellationRequested();

            _context.Baskets!
                .FirstOrDefault(basket => basket == user.Basket)!
                .SummUpProducts!
                .Clear();

            _context.Clients!
                .FirstOrDefault(u => u.Equals(user))!
                .Basket!
                .SummUpProducts!
                .Clear();
        }

        public void DeleteProduct(User user, SummUpProduct summUpProduct, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(user);

            ArgumentNullException.ThrowIfNull(summUpProduct);

            token.ThrowIfCancellationRequested();

            _context.Baskets!
                .FirstOrDefault(basket => basket == user.Basket)!
                .SummUpProducts!
                .Remove(summUpProduct);

            _context.Clients!
                .FirstOrDefault(u => u.Equals(user))!
                .Basket!
                .SummUpProducts!
                .Remove(summUpProduct);
        }

        public async Task<IList<Basket>> GetAllBaskets(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            return await _context.Baskets.ToListAsync(token);
        }

        public IList<SummUpProduct> GetAllProductGroupsFromBasket(User user, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            return _context.Clients!
                .FirstOrDefault(u => u.Equals(user))!
                .Basket!
                .SummUpProducts!
                .ToList();
        }

        public Basket GetUserBasket(User user, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(user);

            token.ThrowIfCancellationRequested();

            return _context.Clients.FirstOrDefault(u => u.Equals(user))!.Basket!;
        }

        public async Task SaveChangesAsync(CancellationToken token)
        {
            _logger.LogInformation("BasketsRepository called saving data to DB");

            token.ThrowIfCancellationRequested();

            _ = await _context.SaveChangesAsync(token);
        }

        public void UpdateQuantity(User user, SummUpProduct summUpProduct, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(user);

            ArgumentNullException.ThrowIfNull(summUpProduct);

            token.ThrowIfCancellationRequested();

            _context
        }

        private readonly ILogger<BasketsRepository> _logger;
        private readonly RetailStoreDataContext _context;
    }
}
