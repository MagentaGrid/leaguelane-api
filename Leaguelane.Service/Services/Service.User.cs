using Leaguelane.Enums.Enums;
using Leaguelane.Persistence.Entities;
using Leaguelane.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaguelane.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRepository _repository;
        public UserService(IUserRepository userRepository, IRepository repository)
        {
            _userRepository = userRepository;
            _repository = repository;
        }

        public async Task<List<User>> GetAllUsers(CancellationToken cancellationToken)
        {
            return (await _repository.GetAllAsync<User>()).ToList();
        }

        public async Task<User> GetUserById(int id, CancellationToken cancellationToken)
        {
            return await _userRepository.GetUserById(id, cancellationToken);
        }

        public async Task<User> CreateUser(User user, CancellationToken cancellationToken)
        {
            return await _userRepository.CreateUser(user, cancellationToken);
        }

        public async Task<User> UpdateUser(User user, CancellationToken cancellationToken)
        {
            return await _userRepository.UpdateUser(user, cancellationToken);
        }

        public async Task<bool> IsUserNameExists(string userName, CancellationToken cancellationToken)
        {
            return await _userRepository.IsUserNameExists(userName, cancellationToken);
        }

        public async Task<bool> IsUserNameExistsForUpdate(string userName, int userId, CancellationToken cancellationToken)
        {
            return await _userRepository.IsUserNameExistsForUpdate(userName, userId, cancellationToken);
        }

        public async Task<User> AuthenticateUser(string userName, string password, CancellationToken cancellationToken)
        {
            return await _userRepository.AuthenticateUser(userName, password, cancellationToken);
        }

        public async Task<User> GetUserByUserName(string userName, CancellationToken cancellationToken)
        {
            return await _repository.FirstOrDefaultAsync<User>(x => x.UserName == userName, cancellationToken);
        }
    }
}
