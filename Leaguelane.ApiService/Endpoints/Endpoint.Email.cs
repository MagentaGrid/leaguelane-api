using Leaguelane.Api.Handlers;
using Leaguelane.Constants.Enums;
using Leaguelane.Models.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Leaguelane.Api.Endpoints
{
    public static class EmailEndpoint
    {
        public static RouteGroupBuilder AddEmailRoutes(this RouteGroupBuilder group)
        {
            group.MapPost("", SendEmail).WithName("email-send");
            return group;
        }

        public static async Task<IResult> SendEmail(ISender sender, [FromBody] EmailDto email, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new SendEmailCommand(email), cancellationToken);
            return TypedResults.Ok(result);
        }
    }
}
