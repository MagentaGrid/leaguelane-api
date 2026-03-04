using Leaguelane.Persistence.Entities;
using Leaguelane.Repository.Repositories;

namespace Leaguelane.Service.Services
{
    public class PasswordResetTokenService: IPasswordResetTokenService
    {
        private readonly IRepository _repo;

        public PasswordResetTokenService(IRepository repo)
        {
            _repo = repo;
        }

        public async Task<PasswordResetToken> CreateAsync(PasswordResetToken token, CancellationToken cancellationToken)
        {
            await _repo.AddAsync(token, cancellationToken);
            await _repo.SaveChangesAsync<PasswordResetToken>(cancellationToken);

            return token;
        }

        public async Task<PasswordResetToken> GetTokenByTokenHashAsync(byte[] tokenHash, CancellationToken cancellationToken)
        {
            return await _repo.FirstOrDefaultAsync<PasswordResetToken>(x => x.TokenHash == tokenHash, cancellationToken);
        }

        public async Task<PasswordResetToken> GetTokenByUserIdAsync(int userId, CancellationToken cancellationToken)
        {
            return await _repo.FirstOrDefaultAsync<PasswordResetToken>(x => x.UserId == userId, cancellationToken);
        }

        public async Task<PasswordResetToken> UpdateAsync(PasswordResetToken token, CancellationToken cancellationToken)
        {
            await _repo.UpdateAsync(token);
            await _repo.SaveChangesAsync<PasswordResetToken>(cancellationToken);

            return token;
        }
    }
}
