using Leaguelane.Api.Handlers;
using Leaguelane.Models.Dtos;
using Leaguelane.ApiService.Handlers;
using Leaguelane.Constants.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Leaguelane.ApiService.Endpoints
{
    public static class RoomEndpoint
    {
        public static RouteGroupBuilder AddRoomRoutes(this RouteGroupBuilder group)
        {
            group.MapGet("", GetRooms).WithName("get-all-rooms");
                //.RequireAuthorization(policy => policy.RequireRole(UserRole.Admin.ToString(), UserRole.Employee.ToString()));

            group.MapGet("{id}", GetRoom).WithName("get-room-details");
                //.RequireAuthorization(policy => policy.RequireRole(UserRole.Admin.ToString(), UserRole.Employee.ToString()));

            group.MapPost("", CreateRoom).WithName("room-create")
                .RequireAuthorization(policy => policy.RequireRole(UserRole.Admin.ToString(), UserRole.Employee.ToString()));

            group.MapPut("{id}", UpdateRoom).WithName("room-update")
                .RequireAuthorization(policy => policy.RequireRole(UserRole.Admin.ToString(), UserRole.Employee.ToString()));

            group.MapGet("/availability", CheckCheckAvailability).WithName("room-avilability");
            return group;
        }

        public static async Task<IResult> GetRooms(ISender sender, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new GetRoomsQuery(), cancellationToken);
            return TypedResults.Ok(result);
        }

        public static async Task<IResult> GetRoom(ISender sender, int id, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new GetRoomDetailsQuery(id), cancellationToken);
            return TypedResults.Ok(result);
        }

        public static async Task<IResult> CreateRoom(ISender sender, [FromBody]RoomDto room, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new CreateRoomCommand(room), cancellationToken);
            return TypedResults.Ok(result);
        }

        public static async Task<IResult> UpdateRoom(ISender sender, int id, [FromBody] RoomDto room, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new UpdateRoomCommand(room, id), cancellationToken);
            return TypedResults.Ok(result);

        }    

        public static async Task<IResult> CheckCheckAvailability(DateTime CheckInDate, DateTime CheckOutDate, int NumberOfGuests, ISender sender, CancellationToken cancellationToken)
        {

            BookingAvailabilityDto bookingDto = new BookingAvailabilityDto 
            {
                CheckInDate = DateTime.SpecifyKind(CheckInDate, DateTimeKind.Utc),
                CheckOutDate = DateTime.SpecifyKind(CheckOutDate, DateTimeKind.Utc),
                NumberOfGuests = NumberOfGuests,
            
            };

            var result = await sender.Send(new GetAvailableRoomsQuery(bookingDto), cancellationToken);
            return TypedResults.Ok(result);
        }
    }
}