using Leaguelane.Api.Handlers;
using Leaguelane.Constants.Enums;
using Leaguelane.Models.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Leaguelane.Api.Endpoints
{
    public static class RoomTypeEndpoint
    {
        public static RouteGroupBuilder AddRoomTypeRoutes(this RouteGroupBuilder group)
        {
            group.MapGet("", GetRoomTypes).WithName("roomtypes")
                .RequireAuthorization(policy => policy.RequireRole(UserRole.Admin.ToString(), UserRole.Employee.ToString()));

            group.MapGet("{id}", GetRoomTypeDetails).WithName("roomtype")
                .RequireAuthorization(policy => policy.RequireRole(UserRole.Admin.ToString(), UserRole.Employee.ToString()));

            group.MapPost("", CreateRoom).WithName("roomtypes-create")
                .RequireAuthorization(policy => policy.RequireRole(UserRole.Admin.ToString(), UserRole.Employee.ToString()));

            group.MapPut("{id}", UpdateRoom).WithName("roomtypes-update")
                .RequireAuthorization(policy => policy.RequireRole(UserRole.Admin.ToString(), UserRole.Employee.ToString()));

            return group;
        }

        public static async Task<IResult> GetRoomTypes(ISender sender, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new GetRoomTypesQuery(), cancellationToken);
            return TypedResults.Ok(result);
        }

        public static async Task<IResult> GetRoomTypeDetails(ISender sender, int id, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new GetRoomTypeDetailsQuery(id), cancellationToken);
            return TypedResults.Ok(result);
        }

        public static async Task<IResult> CreateRoom(ISender sender, [FromBody]RoomTypeDto roomType, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new CreateRoomTypeCommand(roomType), cancellationToken);
            return TypedResults.Ok(result);
        }

        public static async Task<IResult> UpdateRoom(ISender sender, int id, [FromBody]RoomTypeDto roomType, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new UpdateRoomTypeCommand(roomType, id), cancellationToken);
            return TypedResults.Ok(result);
        }
    }
}
