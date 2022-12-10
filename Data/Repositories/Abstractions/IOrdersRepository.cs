using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Abstractions;

public interface IOrdersRepository
{
    void AddOrder(User user, Order order, CancellationToken token);

    void UpdateOrder(Order order, CancellationToken token);

    void ConfirmOrder(User user, Order order, CancellationToken token);

    void PayOrder(User user, Order order, CancellationToken token);

    void TakeOrder(User user, Order order, CancellationToken token);

    void CancelOrder(User user, Order order, CancellationToken token);

    Task<bool> Find(Order order, CancellationToken token);

    Task<IList<Order>> GetAllOrders(CancellationToken token);

    IList<Order> GetAllUserOrders(User user, CancellationToken token);

    IList<Order> GetAllShopOrders(Shop shop, CancellationToken token);

    Task SaveChangesAsync(CancellationToken token);
}
