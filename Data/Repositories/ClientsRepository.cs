﻿using Castle.Core.Logging;
using Data.Repositories.Abstractions;
using Models;
using Data.Contexts;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Data.Contexts.Abstractions;

namespace Data.Repositories;

public sealed class ClientsRepository
    : IClientsRepository
{
    public ClientsRepository(
        ILogger<ClientsRepository> logger,
        IRepositoryContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

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
    private readonly IRepositoryContext _context;

}
