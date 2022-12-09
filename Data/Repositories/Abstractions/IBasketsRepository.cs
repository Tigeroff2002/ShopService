using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Abstractions
{
    public interface IBasketsRepository
    {
        Task AddProduct(User user, Product product, CancellationToken token);

        void UpdateQuantity(User user, SummUpProduct summUpProduct, CancellationToken token);

        void DeleteProduct(User user, SummUpProduct summUpProduct, CancellationToken token);

        void ClearBasket(User user, CancellationToken token);

        Task<IList<Basket>> GetAllBaskets(CancellationToken token);

        Task<IList<SummUpProduct>> GetAllProducts(User user, CancellationToken token);

        Task SaveChangesAsync(CancellationToken token);
    }
}
