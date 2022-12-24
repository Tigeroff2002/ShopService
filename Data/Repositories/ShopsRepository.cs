using Data.Contexts;
using Data.Contexts.Abstractions;
using Data.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Models;

namespace Data.Repositories;

public sealed class ShopsRepository
    : IShopsRepository
{
    public ShopsRepository(
        ILogger<ShopsRepository> logger,
        IRepositoryContext context)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _context = context ?? throw new ArgumentNullException(nameof(context));

        _logger.LogInformation("ShopsRepository has created just now");
    }

    public void Add(Shop shop, SummUpProduct summUpProduct, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(shop);

        ArgumentNullException.ThrowIfNull(summUpProduct);

        token.ThrowIfCancellationRequested();

        _context.Shops
            .FirstOrDefault(s => s.Equals(shop))!
            .ProductQuantities!
            .Add(summUpProduct);

        //_context.Entry(shop).State = EntityState.Modified;
    }

    public void ClearShop(Shop shop, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(shop);

        token.ThrowIfCancellationRequested();

        _context.Shops
            .FirstOrDefault(s => s.Equals(shop))!
            .ProductQuantities!
            .Clear();

        //_context.Entry(shop).State = EntityState.Modified;
    }

    public void Delete(Shop shop, SummUpProduct summUpProduct, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(shop);

        ArgumentNullException.ThrowIfNull(summUpProduct);

        token.ThrowIfCancellationRequested();

        _context.Shops
            .FirstOrDefault(s => s.Equals(shop))!
            .ProductQuantities!
            .Remove(summUpProduct);

        //_context.Entry(shop).State = EntityState.Modified;
    }

    public async Task<IList<Shop>> GetAllShops(CancellationToken token)
    {
        token.ThrowIfCancellationRequested();

        return await _context.Shops.ToListAsync();
    }

    public int GetSummaryProductsOneTypeExistense(Shop shop, Product product, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public void SaveChanges()
    {
        _logger.LogInformation("OrdersRepository called a saving data to DB");

         _context.SaveChanges();
    }

    public void Update(Shop shop, SummUpProduct summUpProduct, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    private readonly ILogger<ShopsRepository> _logger;
    private readonly IRepositoryContext _context;
}
