using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Abstractions;

public interface IClientsRepository
{
    Task AddAsync(User user, CancellationToken token);

    void Update(User user, CancellationToken token);

    void Delete(User user, CancellationToken token);

    Task<User> FindAsync(int userId, CancellationToken token);

    Task<User> FindNickNameAsync(string nickName, CancellationToken token);

    Task<Basket> TakeBasket(int userId, CancellationToken token);

    Task<IList<User>> GetAllUsers(CancellationToken token);

    Task<IList<User>> GetAllUsersByRole(RoleType type, CancellationToken token);

    int UserCount { get; }

    void SaveChanges();
}
