using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Leaguelane.Persistence.Entities
{
    public class PasswordResetToken
    {
        [Key]
        public int PasswordResetTokenId { get; set; }
        public int UserId { get; set; }
        public byte[] TokenHash { get; set; } = default!;
        public DateTime ExpiresAt { get; set; }
        public bool Used { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
