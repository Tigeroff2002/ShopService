using Castle.Core.Logging;
using Data.Repositories.Abstractions;
using Models;
using Data.Contexts;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Data.Contexts.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Data.Repositories;

public sealed class ClientsRepository
    : IClientsRepository
{
    public ClientsRepository(
        ILogger<ClientsRepository> logger,
        IServiceProvider provider)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _provider = provider ?? throw new ArgumentNullException(nameof(provider));

        using var scope = provider.GetRequiredService<IServiceScopeFactory>().CreateScope();
        _context = scope.ServiceProvider.GetRequiredService<IRepositoryContext>();

        _logger.LogInformation("ClientsRepository was created just now");
    }
    public async Task AddAsync(User user, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(user);

        token.ThrowIfCancellationRequested();

        _ = await _context.Clients.AddAsync(user, token);
    }

    public void Delete(User user, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(user);

        token.ThrowIfCancellationRequested();

        _ = _context.Clients.Remove(user);
    }

    public async Task<User> FindAsync(int userId, CancellationToken token)
    {
        token.ThrowIfCancellationRequested();

        var findedUser = await _context.Clients
            .FindAsync(
                new object?[] { userId },
                    token)
            .ConfigureAwait(false);

        return findedUser!;
    }

    public async Task<User> FindAsync(string email, CancellationToken token)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException(nameof(email));
        }

        token.ThrowIfCancellationRequested();

        // but maybe this dont works cause email is not pk
        var findedUser = await _context.Clients
            .FindAsync(
                new object?[] { email },
                    token)
            .ConfigureAwait(false);

        return findedUser!;
    }

    public async Task<Basket> TakeBasket(int userId, CancellationToken token)
    {
        token.ThrowIfCancellationRequested();

        var findedUser = await _context.Clients
            .FindAsync(
                new object?[] { userId },
                    token)
            .ConfigureAwait(false);

        ArgumentNullException.ThrowIfNull(findedUser);

        var takenBasket = await _context.Baskets.FirstOrDefaultAsync(b => 
            b.Client!.Equals(findedUser), 
                CancellationToken.None)
            .ConfigureAwait(false);

        return takenBasket!;
    }

    public async Task<IList<User>> GetAllUsers(CancellationToken token)
    {
        token.ThrowIfCancellationRequested();

        return await _context.Clients.ToListAsync(token);
    }

    public async Task<IList<User>> GetAllUsersByRole(RoleType type, CancellationToken token)
    {
        token.ThrowIfCancellationRequested();

        return await _context.Clients
            .Where(user => user.Role!.RoleType == type)
            .ToListAsync(token);
    }

    public void SaveChanges()
    {
        _logger.LogInformation("ClientsRepository called saving data to DB");

        _context.SaveChanges();
    }

    public void Update(User user, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(user);

        token.ThrowIfCancellationRequested();

        //_context.Entry(user).State = EntityState.Modified;
    }

    public int UserCount 
        => _context.Clients.Count();

    private readonly ILogger<ClientsRepository> _logger;
    private readonly IServiceProvider _provider;
    private readonly IRepositoryContext _context;

}
