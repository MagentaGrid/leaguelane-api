using Leaguelane.Api.Handlers;
using Leaguelane.Constants.Enums;
using Leaguelane.Models.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Leaguelane.Api.Endpoints
{
    public static class BookingEndpoint
    {
        public static RouteGroupBuilder AddBookingRoutes(this RouteGroupBuilder group)
        {
            group.MapGet("", GetBookings).WithName("bookings")
                .RequireAuthorization(policy => policy.RequireRole(UserRole.Admin.ToString(), UserRole.Employee.ToString(), UserRole.User.ToString()));

            group.MapGet("{id}", GetBooking).WithName("booking")
                .RequireAuthorization(policy => policy.RequireRole(UserRole.Admin.ToString(), UserRole.Employee.ToString(), UserRole.User.ToString()));

            group.MapPost("", CreateBooking).WithName("booking-create");
                //.RequireAuthorization(policy => policy.RequireRole(UserRole.Admin.ToString(), UserRole.Employee.ToString(), UserRole.User.ToString()));

            group.MapPut("{id}", UpdateBooking).WithName("booking-update")
                .RequireAuthorization(policy => policy.RequireRole(UserRole.Admin.ToString(), UserRole.Employee.ToString(), UserRole.User.ToString()));

            return group;
        }

        public static async Task<IResult> GetBookings(ISender sender, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new GetBookingsQuery(), cancellationToken);
            return TypedResults.Ok(result);
        }

        public static async Task<IResult> GetBooking(ISender sender, int id, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new GetBookingDetailsQuery(id), cancellationToken);
            return TypedResults.Ok(result);
        }

        public static async Task<IResult> CreateBooking(ISender sender, [FromBody] BookingDto booking, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new CreateBookingCommand(booking), cancellationToken);
            return TypedResults.Ok(result);
        }

        public static async Task<IResult> UpdateBooking(ISender sender, int id, [FromBody] BookingDto booking, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new UpdateBookingCommand(booking, id), cancellationToken);
            return TypedResults.Ok(result);
        }
    }
}
