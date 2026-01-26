using Leaguelane.Constants.Enums;
using Leaguelane.Persistence.Entities;
using System.ComponentModel.DataAnnotations;

namespace Leaguelane.Models.Dtos
{
    public class UserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public UserRole Role { get; set; }
        public string? PhoneNumber { get; set; }
        public string? PhoneNumerPrefix { get; set; }
        public string? Address { get; set; }


        public string Password { get; set; }

        public string UserName { get; set; }
        public int UserId { get; set; }
        public bool? Active { get; set; }
    }

    public record UserResponse
    (
         bool IsSuccess,
         string? ErrorMessage,
         User? User,
         string? Token
    );

    public class UsersResponse : Response
    {
        public List<User>? Users { get; set; }
    }

    public class LoginRequestDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public record LoginResponse
    (
        bool IsSuccess,
        string? ErrorMessage,
        LoginReponseDto? User,
        string? Token
    );
    public class UserRequest
    {
        public IEnumerable<User> Users { get; set; }
        public string? FirstName { get; set; }
        public string? SecondName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public UserRole? UserRole { get; set; }
        public string? ImageUrl { get; set; }
        public bool? Active { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }

    public class LoginReponseDto
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserRole Role { get; set; }
    }
}
