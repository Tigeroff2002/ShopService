using Models;

namespace Data.Repositories.Abstractions;

public interface IShopsRepository
{
    void Add(Shop shop, SummUpProduct summUpProduct, CancellationToken token);

    void Update(Shop shop, SummUpProduct summUpProduct, CancellationToken token);

    void Delete(Shop shop, SummUpProduct summUpProduct, CancellationToken token);

    void ClearShop(Shop shop, CancellationToken token);

    Task<IList<Shop>> GetAllShops(CancellationToken token);

    int GetSummaryProductsOneTypeExistense(Shop shop, Product product, CancellationToken token);

    void SaveChanges();
}
