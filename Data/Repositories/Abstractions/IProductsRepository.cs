using Models;

namespace Data.Repositories.Abstractions;

public interface IProductsRepository
{
    Task Add(Product product, CancellationToken token);

    void Update(Product product, CancellationToken token);

    void Delete(Product product, CancellationToken token);

    Task<IList<Product>> GetAllProducts(CancellationToken token);

    Task SaveChangesAsync(CancellationToken token);

}
