using Leaguelane.Persistence.Entities;

namespace Leaguelane.Service.Services
{
    public interface IPasswordResetTokenService
    {
        Task<PasswordResetToken> CreateAsync(PasswordResetToken token, CancellationToken cancellationToken);
        Task<PasswordResetToken> GetTokenByTokenHashAsync(byte[] tokenHash, CancellationToken cancellationToken);
        Task<PasswordResetToken> GetTokenByUserIdAsync(int userId, CancellationToken cancellationToken);
        Task<PasswordResetToken> UpdateAsync(PasswordResetToken token, CancellationToken cancellationToken);
    }
}
