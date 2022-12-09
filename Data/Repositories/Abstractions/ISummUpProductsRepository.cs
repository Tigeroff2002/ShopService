using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Abstractions
{
    public interface ISummUpProductsRepository
    {
        Task Add(SummUpProduct product, CancellationToken token);

        void Update(SummUpProduct summUpProduct, CancellationToken token);

        void Delete(SummUpProduct summUpProduct, CancellationToken token);

        Task SaveChangesAsync(CancellationToken token);
    }
}
