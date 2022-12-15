using Data.Repositories.Abstractions;
using Data.Contexts;
using Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Data.Contexts.Abstractions;

namespace Data.Repositories;

public sealed class ProductsRepository
    : IProductsRepository
{
    public ProductsRepository(
        ILogger<ProductsRepository> logger,
        IRepositoryContext context)
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

    public void SaveChanges()
    {
        _logger.LogInformation("ProductsRepository called saving data to DB");

        _context.SaveChanges();
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

    private readonly ILogger<ProductsRepository> _logger;
    private readonly IRepositoryContext _context;
}
