using Leaguelane.Constants.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaguelane.Persistence.Entities
{
    public class User:Entity
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string? Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserRole Role { get; set; }
        public string? PhoneNumber { get; set; }
        public string? PhoneNumerPrefix { get; set; }
        public string? Address { get; set; }
    }
}
