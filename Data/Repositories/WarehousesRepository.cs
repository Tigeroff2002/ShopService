using Data.Contexts;
using Data.Contexts.Abstractions;
using Data.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;

namespace Data.Repositories;

public sealed class WarehousesRepository
    : IWarehousesRepository
{
    public WarehousesRepository(
       ILogger<WarehousesRepository> logger,
       IRepositoryContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        _logger.LogInformation("ShopsRepository has created just now");
    }

    public void Add(Warehouse warehouse, SummUpProduct summUpProduct, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(warehouse);

        ArgumentNullException.ThrowIfNull(summUpProduct);

        token.ThrowIfCancellationRequested();

        _context.Shops
            .FirstOrDefault(s => s.Equals(warehouse))!
            .ProductQuantities!
            .Add(summUpProduct);

        //_context.Entry(warehouse).State = EntityState.Modified;
    }

    public void ClearWarehouse(Warehouse warehouse, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(warehouse);

        token.ThrowIfCancellationRequested();

        _context.Shops
            .FirstOrDefault(s => s.Equals(warehouse))!
            .ProductQuantities!
            .Clear();

        //_context.Entry(warehouse).State = EntityState.Modified;
    }

    public void Delete(Warehouse warehouse, SummUpProduct summUpProduct, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(warehouse);

        ArgumentNullException.ThrowIfNull(summUpProduct);

        token.ThrowIfCancellationRequested();

        _context.Shops
            .FirstOrDefault(s => s.Equals(warehouse))!
            .ProductQuantities!
            .Remove(summUpProduct);

        //_context.Entry(warehouse).State = EntityState.Modified;
    }

    public async Task<IList<Shop>> GetAllWarehouses(CancellationToken token)
    {
        token.ThrowIfCancellationRequested();

        return await _context.Shops.ToListAsync();
    }

    public int GetSummaryProductsOneTypeExistense(Warehouse warehouse, Product product, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public void SaveChanges()
    { 
        _logger.LogInformation("OrdersRepository called a saving data to DB");

        _context.SaveChanges();
    }

    public void Update(Warehouse warehouse, SummUpProduct summUpProduct, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    private readonly ILogger<WarehousesRepository> _logger;
    private readonly IRepositoryContext _context;
}
