using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Abstractions;

public interface IOrdersRepository
{
    Task AddOrderAsync(Order order, CancellationToken cancellationToken);

    void AddOrder(User user, Order order, CancellationToken token);

    void UpdateOrder(Order order, CancellationToken token);

    void ConfirmOrder(User user, Order order, CancellationToken token);

    void PayOrder(User user, Order order, CancellationToken token);

    void TakeOrder(User user, Order order, CancellationToken token);

    void CancelUserOrder(User user, Order order, CancellationToken token);

    void CancelOrder(Order order, CancellationToken token);

    Task<bool> FindAsync(Order order, CancellationToken token);

    Task<IList<Order>> GetAllOrders(CancellationToken token);

    IList<Order> GetAllUserOrders(User user, CancellationToken token);

    IList<Order> GetAllShopOrders(Shop shop, CancellationToken token);

    void SaveChanges();
}
