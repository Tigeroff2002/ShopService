using Data.Repositories.Abstractions;
using Data.Contexts;
using Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Data.Contexts.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Data.Repositories;

public sealed class ProductsRepository
    : IProductsRepository
{
    public ProductsRepository(
        ILogger<ProductsRepository> logger,
        IServiceProvider provider)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _provider = provider ?? throw new ArgumentNullException(nameof(provider));

        using var scope = provider.GetRequiredService<IServiceScopeFactory>().CreateScope();
        _context = scope.ServiceProvider.GetRequiredService<IRepositoryContext>();

        _logger.LogInformation("ProductsRepository was created just now");
    }
    public async Task Add(Product product, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(product);

        token.ThrowIfCancellationRequested();

        _ = await _context.Products.AddAsync(product, token);
    }

    public void Delete(int id, CancellationToken token)
    {
        token.ThrowIfCancellationRequested();

        var product = _context.Products.First(x => x.Id == id);

        _ = _context.Products.Remove(product);
    }

    public void Update(Product product, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(product);

        token.ThrowIfCancellationRequested();

        //_context.Entry(product).State = EntityState.Modified;
    }

    public async Task<bool> Find(Product product, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(product);

        token.ThrowIfCancellationRequested();

        var findedProduct = await _context.Products.FindAsync(product, token);

        return findedProduct == null ? false : true;
    }

    public async Task<List<Product>> GetAllProducts(CancellationToken token)
    {
        token.ThrowIfCancellationRequested();

        return await _context.Products
            .ToListAsync(token);
    }

    public async Task<List<Product>> GetProductsByDeviceType(int deviceTypeId, CancellationToken token)
    {
        token.ThrowIfCancellationRequested();

        return await _context.Products
            .Where(p => p.DeviceType!.Id == deviceTypeId)
            .ToListAsync(token);
    }

    public async Task<List<Product>> GetProductsByProducer(int producerId, CancellationToken token)
    {
        token.ThrowIfCancellationRequested();

        return await _context.Products
            .Where(p => p.Producer!.Id == producerId)
            .ToListAsync(token);
    }

    public async Task<List<Product>> GetProductsOnExistense(CancellationToken token)
    {
        token.ThrowIfCancellationRequested();

        return await _context.Products
            .Where(p => p.SummUpProducts != null)
            .ToListAsync(token);
    }

    public async Task<List<Product>> GetProductsWithMarkAbove(int rating, CancellationToken token)
    {
        token.ThrowIfCancellationRequested();

        return await _context.Products
            .Where(p => p.Rating > rating)
            .ToListAsync(token);
    }

    public async Task<List<Product>> GetProductsWithCostBelow(int cost, CancellationToken token)
    {
        token.ThrowIfCancellationRequested();

        return await _context.Products
            .Where(p => p.Cost < cost)
            .ToListAsync(token);
    }

    public async Task<Product> FindProduct(int id, CancellationToken token)
    {
        token.ThrowIfCancellationRequested();

        return await _context.Products.FirstOrDefaultAsync(p => p.Id == id, token)
            .ConfigureAwait(false);
    }

    public void SaveChanges()
    {
        _logger.LogInformation("ProductsRepository called saving data to DB");

        _context.SaveChanges();
    }



    private readonly ILogger<ProductsRepository> _logger;
    private readonly IServiceProvider _provider;
    private readonly IRepositoryContext _context;
}
