using Models;

namespace Data.Repositories.Abstractions;

public interface IOrdersRepository
{
    Task AddOrderAsync(Order order, CancellationToken cancellationToken);

    void AddOrder(User user, Order order, CancellationToken token);

    void UpdateOrder(Order order, CancellationToken token);

    void DeleteOrder(Order order, CancellationToken token);

    void ConfirmOrder(User user, Order order, CancellationToken token);

    void PayOrder(User user, Order order, CancellationToken token);

    void TakeOrder(User user, Order order, CancellationToken token);

    void CancelUserOrder(User user, Order order, CancellationToken token);

    void CancelOrder(Order order, CancellationToken token);

    Order Find(int id, CancellationToken token);

    Order FindOrder(int clientId, CancellationToken token);

    Task<Order> FindOrderAsync(Order order, CancellationToken token);

    Task<List<Order>> GetAllOrders(CancellationToken token);

    List<Order> GetAllUserOrders(User user, CancellationToken token, bool skipLast);

    List<Order> GetAllShopOrders(Shop shop, CancellationToken token);

    void SaveChanges();
}
