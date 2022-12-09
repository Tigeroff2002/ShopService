using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Abstractions
{
    public interface IShopsRepository
    {
        Task Add(Product product, CancellationToken token);

        void Update(SummUpProduct summUpProduct, CancellationToken token);

        void Delete(SummUpProduct summUpProduct, CancellationToken token);

        void ClearShop(CancellationToken token);

        Task<IList<SummUpProduct>> GetAllProducts(CancellationToken token);

        Task SaveChangesAsync(CancellationToken token);
    }
}
