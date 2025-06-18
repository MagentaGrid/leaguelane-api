using Leaguelane.Models.Dtos;
using Leaguelane.Persistence.Entities;
using Leaguelane.Service.Services;
using MediatR;

namespace Leaguelane.Api.Handlers
{
    public record GetUsersQuery : IRequest<UsersResponse>;
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, UsersResponse>
    {
        private readonly IUserService _userService;
        public GetUsersQueryHandler(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<UsersResponse> Handle(GetUsersQuery query, CancellationToken cancellationToken)
        {
            var usersResponse = new UsersResponse
            
            { 
                ErrorMessage=null,
                IsSuccess=true,
                Users= await _userService.GetAllUsers(cancellationToken)

            };

            return usersResponse;


        }
    }
}
