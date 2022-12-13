using Data.Repositories.Abstractions;
using Data.Contexts;
using Models;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

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

    public async Task AddOrderAsync(Order order, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(order);

        token.ThrowIfCancellationRequested();

        await _context.AddAsync(order, token).ConfigureAwait(false);
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

    public void CancelUserOrder(User user, Order order, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(user);

        ArgumentNullException.ThrowIfNull(order);

        token.ThrowIfCancellationRequested();

        _context.Entry(order).State = EntityState.Deleted;

        _context.Orders.Remove(order);

        _context.Clients
            .FirstOrDefault(u => u.Equals(user))!
            .Orders!
            .Remove(order);
    }

    public void CancelOrder(Order order, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(order);

        token.ThrowIfCancellationRequested();

        _context.Orders.Remove(order);
    }

    public void ConfirmOrder(User user, Order order, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(user);

        ArgumentNullException.ThrowIfNull(order);

        token.ThrowIfCancellationRequested();

        _context.Clients
            .FirstOrDefault(u => u.Equals(user))!
            .Orders!
            .FirstOrDefault(o => o.Equals(order))!
            .ConfirmCreatedOrder();

        _context.Entry(order).State = EntityState.Modified;
    }

    public async Task<bool> FindAsync(Order order, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(order);

        token.ThrowIfCancellationRequested();

        var findedOrder = await _context.Orders.FindAsync(order);

        return findedOrder == null ? false : true;

    }

    public async Task<IList<Order>> GetAllOrders(CancellationToken token)
    {
        token.ThrowIfCancellationRequested();

        return await _context.Orders.ToListAsync();
    }

    public IList<Order> GetAllShopOrders(Shop shop, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public IList<Order> GetAllUserOrders(User user, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public void PayOrder(User user, Order order, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(user);

        ArgumentNullException.ThrowIfNull(order);

        token.ThrowIfCancellationRequested();

        _context.Clients
            .FirstOrDefault(u => u.Equals(user))!
            .Orders!
            .FirstOrDefault(o => o.Equals(order))!
            .PayConfirmedOrder();

        _context.Entry(order).State = EntityState.Modified;
    }

    public void TakeOrder(User user, Order order, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(user);

        ArgumentNullException.ThrowIfNull(order);

        token.ThrowIfCancellationRequested();

        _context.Clients
            .FirstOrDefault(u => u.Equals(user))!
            .Orders!
            .FirstOrDefault(o => o.Equals(order))!
            .isGot = true;

        _context.Entry(order).State = EntityState.Modified;
    }

    public void UpdateOrder(Order order, CancellationToken token)
    {

        ArgumentNullException.ThrowIfNull(order);

        token.ThrowIfCancellationRequested();

        _context.Entry(order).State = EntityState.Modified;
    }

    public async Task SaveChangesAsync(CancellationToken token)
    {
        token.ThrowIfCancellationRequested();

        _logger.LogInformation("OrdersRepository called a saving data to DB");

        _ = await _context.SaveChangesAsync(token);
    }

    private readonly ILogger<OrdersRepository> _logger;
    private readonly RetailStoreDataContext _context;
}
