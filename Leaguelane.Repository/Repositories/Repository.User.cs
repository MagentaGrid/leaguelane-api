using Leaguelane.Persistence.Context;
using Leaguelane.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaguelane.Repository.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly LeaguelaneDbContext _context;
        public UserRepository(LeaguelaneDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllUsers(CancellationToken cancellationToken)
        {
            return await _context.Users.Where(x => x.Active == true).ToListAsync(cancellationToken);
        }

        public async Task<User> GetUserById(int id, CancellationToken cancellationToken)
        {
            return await _context.Users.Where(x => x.UserId == id && x.Active == true).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<User> CreateUser(User user, CancellationToken cancellationToken)
        {
            var passwordHasher = new PasswordHasher<User>();
            user.Password = passwordHasher.HashPassword(user, user.Password);
            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);
            return user;
        }

        public async Task<User> UpdateUser(User user, CancellationToken cancellationToken)
        {
            var passwordHasher = new PasswordHasher<User>();
            user.Password = passwordHasher.HashPassword(user, user.Password);
            _context.Users.Update(user);
            await _context.SaveChangesAsync(cancellationToken);
            return user;
        }

        public async Task<bool> IsUserNameExists(string userName, CancellationToken cancellationToken)
        {
            return await _context.Users.AnyAsync(x => x.UserName == userName && x.Active == true, cancellationToken);
        }

        public async Task<bool> IsUserNameExistsForUpdate(string userName, int userId, CancellationToken cancellationToken)
        {
            return await _context.Users.AnyAsync(x => x.UserName == userName && x.Active == true && x.UserId != userId, cancellationToken);
        }

        public async Task<User> AuthenticateUser(string userName, string password, CancellationToken cancellationToken)
        {
            var passwordHasher = new PasswordHasher<User>();
            
            var user = await _context.Users.Where(x => x.UserName == userName && x.Active == true).FirstOrDefaultAsync(cancellationToken);

            if (user == null)
            {
                return null;
            }

            var result = passwordHasher.VerifyHashedPassword(
                user,
                user.Password,
                password
            );

            if (result == PasswordVerificationResult.Success)
            {
                return user;
            }
            else
            {
                return null;
            }
        }
    }
}
