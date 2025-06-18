using Leaguelane.Models.Dtos;
using Leaguelane.Persistence.Entities;
using Leaguelane.Service.Services;
using MediatR;

namespace Leaguelane.Api.Handlers
{
    public record CreateUserCommand(UserDto user) : IRequest<UserResponse>;
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserResponse>
    {
        private readonly IUserService _userService;
        public CreateUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<UserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (await _userService.IsUserNameExists(request.user.UserName, cancellationToken))
                    return new UserResponse(false, "Username already exists", null, null);

                var user = await _userService.CreateUser(MapUserDtoToUser(request.user), cancellationToken);

                return new UserResponse(true, "User created successfully", user, "Dummy token");
            }
            catch (Exception ex)
            {
                return new UserResponse(false, ex.Message, null, null);
            }
        }

        private static User MapUserDtoToUser(UserDto userDto)
        {
            return new User
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Password = userDto.Password,
                UserName = userDto.UserName,
                Active = true,
                Role = userDto.Role,
                PhoneNumber = userDto.PhoneNumber,
                PhoneNumerPrefix = userDto.PhoneNumerPrefix,
                Address = userDto.Address,
                Created = DateTime.UtcNow
            };
        }
    }
}
