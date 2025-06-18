using Leaguelane.Persistence.Entities;
using Leaguelane.Service.Services;
using MediatR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Data;
using System.Net;
using System;
using Leaguelane.Models.Dtos;

namespace Leaguelane.Api.Handlers
{
    public record UpdateUserCommand(UserDto user, int UserId) : IRequest<UserResponse>;
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserResponse>
    {
        private readonly IUserService _userService;
        public UpdateUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<UserResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existingUser = await _userService.GetUserById(request.UserId, cancellationToken);
                if(existingUser == null)
                {
                    return new UserResponse(false, "User not found", null, "Dummy");
                }

                if (await _userService.IsUserNameExistsForUpdate(request.user.UserName, request.user.UserId, cancellationToken))
                {
                    return new UserResponse(false, "Username already exists", null, "Dummy");
                }

                var result = await _userService.UpdateUser(MapUserDtoToUser(request.user, existingUser), cancellationToken);

                return new UserResponse(true, "User updated successfully", result, "Dummy");
            }
            catch (Exception ex)
            {
                return new UserResponse(false, ex.Message, null, "Dummy");
            }
        }

        private static User MapUserDtoToUser(UserDto userDto, User user)
        {
            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.Password = userDto.Password;
            user.UserName = userDto.UserName;
            user.Active = userDto.Active;
            user.UserId = userDto.UserId;
            user.Role = userDto.Role;
            user.PhoneNumber = userDto.PhoneNumber;
            user.PhoneNumerPrefix = userDto.PhoneNumerPrefix;
            user.Address = userDto.Address;
            user.Updated = DateTime.UtcNow;

            return user;
        }
    }
}
