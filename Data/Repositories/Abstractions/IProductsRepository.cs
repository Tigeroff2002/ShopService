using Models;

namespace Data.Repositories.Abstractions;

public interface IProductsRepository
{
    Task Add(Product product, CancellationToken token);

    void Update(Product product, CancellationToken token);

    Task<bool> Find(Product product, CancellationToken token);

    Task<Product> FindProduct(int id, CancellationToken token);

    void Delete(int id, CancellationToken token);

    Task<List<Product>> GetAllProducts(CancellationToken token);

    Task<List<Product>> GetProductsByDeviceType(int deviceTypeId, CancellationToken token);

    Task<List<Product>> GetProductsByProducer(int producerId, CancellationToken token);

    Task<List<Product>> GetProductsOnExistense(CancellationToken token);

    Task<List<Product>> GetProductsWithMarkAbove(int rating, CancellationToken token);

    Task<List<Product>> GetProductsWithCostBelow(int cost, CancellationToken token);

    void SaveChanges();

}
