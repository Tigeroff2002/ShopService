using Data.Repositories.Abstractions;
using Data.Contexts;
using Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Data.Repositories;

public sealed class ProductsRepository
    : IProductsRepository
{
    public ProductsRepository(
        ILogger<ProductsRepository> logger,
        RetailStoreDataContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        _logger.LogInformation("ProductsRepository was created just now");
    }
    public async Task Add(Product product, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(product);

        token.ThrowIfCancellationRequested();

        _ = await _context.Products.AddAsync(product, token);
    }

    public void Delete(Product product, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(product);

        token.ThrowIfCancellationRequested();

        _ = _context.Products.Remove(product);
    }

    public async Task<IList<Product>> GetAllProducts(CancellationToken token)
    {
        token.ThrowIfCancellationRequested();

        return await _context.Products.ToListAsync(token);
    }

    public async Task SaveChangesAsync(CancellationToken token)
    {
        _logger.LogInformation("ProductsRepository called saving data to DB");

        token.ThrowIfCancellationRequested();

        _ = await _context.SaveChangesAsync(token);
    }

    public void Update(Product product, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(product);

        token.ThrowIfCancellationRequested();

        _context.Entry(product).State = EntityState.Modified;
    }

    public async Task<bool> Find(Product product, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(product);

        token.ThrowIfCancellationRequested();

        var findedProduct = await _context.Products.FindAsync(product, token);

        return findedProduct == null ? false : true;
    }

    private readonly ILogger<ProductsRepository> _logger;
    private readonly RetailStoreDataContext _context;
}
