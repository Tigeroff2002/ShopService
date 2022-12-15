using Microsoft.EntityFrameworkCore;
using Models;

namespace Data.Contexts.Abstractions;

public interface IRepositoryContext
{
    public DbSet<Product> Products { get; }

    public DbSet<User> Clients { get; }

    public DbSet<Order> Orders { get; }

    public DbSet<Warehouse> Warehouses { get; }

    public DbSet<Shop> Shops { get; }

    public DbSet<SummUpProduct> SummUpProducts { get; }

    public DbSet<Basket> Baskets { get; }

    public void SaveChanges();
}
