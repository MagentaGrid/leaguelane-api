using Leaguelane.Models.Dtos;
using Leaguelane.Persistence.Entities;
using Leaguelane.Service.Services;
using MediatR;

namespace Leaguelane.Api.Handlers
{

    public record GetUserDetailsQuery(int UserId) : IRequest<UserResponse>;
    public class GetUserDetailsQueryHandler : IRequestHandler<GetUserDetailsQuery, UserResponse>
    {
        private readonly IUserService _userService;
        public GetUserDetailsQueryHandler(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<UserResponse> Handle(GetUserDetailsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _userService.GetUserById(request.UserId, cancellationToken);
                if (result == null)
                {
                    return new UserResponse(false, "User not found", null, "Dummy token");
                }

                return new UserResponse(true, "User details fetched successfully", result, "Dummy token");
            }
            catch (Exception ex)
            {
                return new UserResponse(false, ex.Message, null, "Dummy token");
            }
        }
    }
}
