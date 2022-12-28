using Data.Repositories.Abstractions;
using Data.Contexts;
using Models;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Data.Contexts.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Data.Repositories;

public sealed class OrdersRepository
   : IOrdersRepository
{
    public OrdersRepository(
        ILogger<OrdersRepository> logger,
        IRepositoryContext context)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _context = context ?? throw new ArgumentNullException(nameof(context));

        _logger.LogInformation("OrdersRepository has created jsut now");
    }

    public async Task AddOrderAsync(Order order, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(order);

        token.ThrowIfCancellationRequested();

        await _context.Orders.AddAsync(order, token).ConfigureAwait(false);
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

        //_context.Entry(order).State = EntityState.Deleted;

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

        //_context.Entry(order).State = EntityState.Modified;
    }

    public Order Find(int id, CancellationToken token)
    {
        token.ThrowIfCancellationRequested();

        var findedOrder = _context.Orders.FirstOrDefault(o => o.Id == id);

        return findedOrder!;

    }

    public async Task<List<Order>> GetAllOrders(CancellationToken token)
    {
        token.ThrowIfCancellationRequested();

        return await _context.Orders.ToListAsync();
    }

    public List<Order> GetAllShopOrders(Shop shop, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public List<Order> GetAllUserOrders(User user, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(user);

        token.ThrowIfCancellationRequested();

        return _context.Clients
            .FirstOrDefault(u => u.Equals(user))!
            .Orders.ToList();
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

        //_context.Entry(order).State = EntityState.Modified;
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

        //_context.Entry(order).State = EntityState.Modified;
    }

    public void UpdateOrder(Order order, CancellationToken token)
    {

        ArgumentNullException.ThrowIfNull(order);

        token.ThrowIfCancellationRequested();

        //_context.Entry(order).State = EntityState.Modified;
    }

    public void SaveChanges()
    {
        _logger.LogInformation("OrdersRepository called a saving data to DB");

        _context.SaveChanges();
    }

    public async Task<Order> FindOrderAsync(Order order, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(order);

        token.ThrowIfCancellationRequested();

        return null!;
    }

    private readonly ILogger<OrdersRepository> _logger;
    private readonly IRepositoryContext _context;
}
