using Leaguelane.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaguelane.Repository.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsers(CancellationToken cancellationToken);
        Task<User> GetUserById(int id, CancellationToken cancellationToken);
        Task<User> CreateUser(User user, CancellationToken cancellationToken);
        Task<User> UpdateUser(User user, CancellationToken cancellationToken);
        Task<bool> IsUserNameExists(string userName, CancellationToken cancellationToken);
        Task<bool> IsUserNameExistsForUpdate(string userName, int userId, CancellationToken cancellationToken);
        Task<User> AuthenticateUser(string userName, string password, CancellationToken cancellationToken);
    }
}
