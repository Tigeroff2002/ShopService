using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Abstractions
{
    public interface IOrdersRepository
    {
        Task Add(User user, Order order, CancellationToken token);

        void ConfirmOrder(User user, Order order, CancellationToken token);

        void PayOrder(User user, Order order, CancellationToken token);

        void TakeOrder(User user, Order order, CancellationToken token);

        void CancelOrder(User user, Order order, CancellationToken token);

        void Find(User user, Order order, CancellationToken token);

        Task<IList<Order>> GetAllOrders(CancellationToken token);

        Task<IList<Order>> GetAllUserOrders(User user, CancellationToken token);

        Task<IList<Order>> GetAllShopOrders(Shop shop, CancellationToken token);

        Task SaveChangesAsync(CancellationToken token);
    }
}
