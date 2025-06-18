using Leaguelane.Api.Handlers;
using Leaguelane.Constants.Enums;
using Leaguelane.Models.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Leaguelane.Api.Endpoints
{
    public static class Endpoint
    {
        public static RouteGroupBuilder AddUserRoutes(this RouteGroupBuilder group)
        {
            group.MapGet("", GetUsers).WithName("users");
                //.RequireAuthorization(policy =>
                //       policy.RequireRole(UserRole.Admin.ToString(), UserRole.Employee.ToString()));

            group.MapGet("{id}", GetUser).WithName("user")
                .RequireAuthorization(policy =>
                        policy.RequireRole(UserRole.Admin.ToString(), UserRole.Employee.ToString(), UserRole.User.ToString()));

            group.MapPost("", CreateUser).WithName("user-create")
                .AllowAnonymous();

            group.MapPut("{id}", UpdateUser).WithName("user-update")
                .RequireAuthorization(policy => 
                        policy.RequireRole(UserRole.Admin.ToString(), UserRole.Employee.ToString(), UserRole.User.ToString()));

            group.MapPost("authenticate", AuthenticateUser).WithName("user-authenticate")
                .AllowAnonymous();

            return group;
        }

        public static async Task<IResult> GetUsers(ISender sender, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new GetUsersQuery(), cancellationToken);
            return TypedResults.Ok(result);
        }

        public static async Task<IResult> GetUser(ISender sender, int id, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new GetUserDetailsQuery(id), cancellationToken);
            return TypedResults.Ok(result);
        }

        public static async Task<IResult> CreateUser(ISender sender, [FromBody]UserDto user, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new CreateUserCommand(user), cancellationToken);
            return TypedResults.Ok(result);
        }

        public static async Task<IResult> UpdateUser(ISender sender, int id, [FromBody]UserDto user, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new UpdateUserCommand(user, id), cancellationToken);
            return TypedResults.Ok(result);
        }
        public static async Task<IResult> AuthenticateUser(ISender sender, [FromBody]UserDto user, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new AuthenticateUserQuery(user), cancellationToken);
            return TypedResults.Ok(result);
        }
    }
}
