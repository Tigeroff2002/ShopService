using Data.Repositories.Abstractions;
using Data.Contexts;
using Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Data.Contexts.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Data.Repositories;

public sealed class BasketsRepository
    : IBasketsRepository
{
    public BasketsRepository(
        ILogger<BasketsRepository> logger, 
        IRepositoryContext context)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _context = context ?? throw new ArgumentNullException(nameof(context));

        _logger.LogInformation("BasketsRepository has created just now");
    }

    public async Task AddBasketAsync(Basket basket, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(basket);

        token.ThrowIfCancellationRequested();

        _ = await _context.Baskets.AddAsync(basket, token);
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

        var basket = _context.Clients!
            .FirstOrDefault(u => u.Equals(user))!
            .Basket!;

        ArgumentNullException.ThrowIfNull(basket);

        _context.Baskets!
            .FirstOrDefault(basket => basket == user.Basket)!
            .SummUpProducts!
            .Clear();

        _context.Clients!
            .FirstOrDefault(u => u.Equals(user))!
            .Basket!
            .SummUpProducts!
            .Clear();

        //_context.Entry(basket).State = EntityState.Modified;
    }

    public void DeleteProductGroup(User user, SummUpProduct summUpProduct, CancellationToken token)
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

        //_context.Entry(summUpProduct).State = EntityState.Deleted;
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

    public void SaveChanges()
    {
        _logger.LogInformation("BasketsRepository called saving data to DB");

        _context.SaveChanges();
    }

    public void UpdateQuantity(User user, SummUpProduct summUpProduct, int changing, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(user);

        ArgumentNullException.ThrowIfNull(summUpProduct);

        token.ThrowIfCancellationRequested();

        var basket = _context.Clients
            .FirstOrDefault(u => u.Equals(user))!
            .Basket!;

        var currentQuantity = _context.Clients
            .FirstOrDefault(u => u.Equals(user))!
            .Basket!
            .SummUpProducts!
            .FirstOrDefault(g => g.Equals(summUpProduct))!
            .Quantity;

        if (currentQuantity + changing > 0)
        {
            _context.Clients
                .FirstOrDefault(u => u.Equals(user))!
                .Basket!
                .SummUpProducts!
                .FirstOrDefault(g => g.Equals(summUpProduct))!
                .Quantity += changing;
        }
        else
        {
            DeleteProductGroup(user, summUpProduct, token);
        }

        //_context.Entry(basket).State = EntityState.Modified;

        //_context.Entry(summUpProduct).State = EntityState.Modified;
    }

    public async Task<bool> Find(Basket basket, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(basket);

        token.ThrowIfCancellationRequested();

        var findedBasket = await _context.Baskets.FindAsync(basket);

        return findedBasket == null ? false : true;
    }

    public async Task<Basket> FindBasket(int clientId, CancellationToken token)
    {
        token.ThrowIfCancellationRequested();

        var findedBasket = await _context.Baskets
            .FirstOrDefaultAsync(x => x.ClientId == clientId)
            .ConfigureAwait(false);

        return findedBasket!;
    }

    private readonly ILogger<BasketsRepository> _logger;
    private readonly IRepositoryContext _context;
}
