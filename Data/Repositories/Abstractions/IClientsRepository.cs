using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Abstractions;

public interface IClientsRepository
{
    Task Add(User user, CancellationToken token);

    void Update(User user, CancellationToken token);

    void Delete(User user, CancellationToken token);

    Task<bool> Find(User user, CancellationToken token);

    Task<IList<User>> GetAllUsers(CancellationToken token);

    Task<IList<User>> GetAllUsersByRole(RoleType type, CancellationToken token);

    void SaveChanges();
}
