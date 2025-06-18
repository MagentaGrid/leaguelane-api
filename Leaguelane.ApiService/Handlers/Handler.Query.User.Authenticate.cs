using Leaguelane.Constants.Enums;
using Leaguelane.Models.Dtos;
using Leaguelane.Service.Services;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Leaguelane.Api.Handlers
{
    public record AuthenticateUserQuery(UserDto user) : IRequest<UserResponse>;
    public class AuthenticateUserQueryHandler : IRequestHandler<AuthenticateUserQuery, UserResponse>
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly IJwtService _jwtService;

        public AuthenticateUserQueryHandler(IUserService userService, IConfiguration configuration, IJwtService jwtService)
        {
            _userService = userService;
            _configuration = configuration;
            _jwtService = jwtService;
        }

        public async Task<UserResponse> Handle(AuthenticateUserQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userService.AuthenticateUser(request.user.UserName, request.user.Password, cancellationToken);

                if (user == null)
                {
                    return new UserResponse(false, "Invalid username or password", null, "Dummy token");
                }

                return new UserResponse(true, "User authenticated successfully", user, _jwtService.GenerateToken(user.UserName, user.Role));
            }
            catch (Exception ex)
            {
                return new UserResponse(false, ex.Message, null, "Dummy token");
            }
        }
    }
}
