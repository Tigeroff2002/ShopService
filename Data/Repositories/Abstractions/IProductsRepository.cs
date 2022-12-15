﻿using Models;

namespace Data.Repositories.Abstractions;

public interface IProductsRepository
{
    Task Add(Product product, CancellationToken token);

    void Update(Product product, CancellationToken token);

    Task<bool> Find(Product product, CancellationToken token);

    void Delete(Product product, CancellationToken token);

    Task<IList<Product>> GetAllProducts(CancellationToken token);

    void SaveChanges();

}
