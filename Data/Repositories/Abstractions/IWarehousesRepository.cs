using Models;

namespace Data.Repositories.Abstractions;

public interface IWarehousesRepository
{
    void Add(Warehouse warehouse, SummUpProduct summUpProduct, CancellationToken token);

    void Update(Warehouse warehouse, SummUpProduct summUpProduct, CancellationToken token);

    void Delete(Warehouse warehouse, SummUpProduct summUpProduct, CancellationToken token);

    void ClearWarehouse(Warehouse warehouse, CancellationToken token);

    Task<IList<Shop>> GetAllWarehouses(CancellationToken token);

    int GetSummaryProductsOneTypeExistense(Warehouse warehouse, Product product, CancellationToken token);

    void SaveChanges();
}