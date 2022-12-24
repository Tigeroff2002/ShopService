using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Abstractions;

public interface IBasketsRepository
{
    Task AddBasketAsync(Basket basket, CancellationToken token);

    void AddProductGroup(User user, SummUpProduct summUpProduct, CancellationToken token);

    void UpdateQuantity(User user, SummUpProduct summUpProduct, int changing, CancellationToken token);

    void DeleteProductGroup(User user, SummUpProduct summUpProduct, CancellationToken token);

    void ClearBasket(User user, CancellationToken token);

    Task<Basket> FindBasket(int clientId, CancellationToken token);

    Task<IList<Basket>> GetAllBaskets(CancellationToken token);

    Basket GetUserBasket(User user, CancellationToken token);

    IList<SummUpProduct> GetAllProductGroupsFromBasket(User user, CancellationToken token);

    void SaveChanges();
}
