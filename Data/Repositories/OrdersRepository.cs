using Data.Repositories.Abstractions;
using Data.Contexts;
using Models;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;

namespace Data.Repositories
{
    public sealed class OrdersRepository
       : IOrdersRepository
    {
        public OrdersRepository(
            ILogger<OrdersRepository> logger,
            RetailStoreDataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _logger.LogInformation("OrdersRepository has created jsut now");
        }

        public void AddOrder(User user, Order order, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(user);

            ArgumentNullException.ThrowIfNull(order);

            token.ThrowIfCancellationRequested();

            _context.Clients
                .FirstOrDefault(u => u.Equals(user))!
                .Orders!
                .Add(order);
        }

        public void CancelOrder(User user, Order order, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public void ConfirmOrder(User user, Order order, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public void Find(User user, Order order, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Order>> GetAllOrders(CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Order>> GetAllShopOrders(Shop shop, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Order>> GetAllUserOrders(User user, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public void PayOrder(User user, Order order, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public async Task SaveChangesAsync(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            _logger.LogInformation("OrdersRepository called a saving data to DB");

            _ = await _context.SaveChangesAsync(token);
        }

        public void TakeOrder(User user, Order order, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        private readonly ILogger<OrdersRepository> _logger;
        private readonly RetailStoreDataContext _context;
    }
}
