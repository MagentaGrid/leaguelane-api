using Leaguelane.Api.Handlers;
using Leaguelane.Constants.Enums;
using Leaguelane.Models.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Leaguelane.Api.Endpoints
{
    public static class SettingsEndpoint
    {
        public static RouteGroupBuilder AddSettingsRoutes(this RouteGroupBuilder group)
        {
            group.MapPut("{id}", UpdateSettings).WithName("settings-update")
                .RequireAuthorization(policy => policy.RequireRole(UserRole.Admin.ToString(), UserRole.Employee.ToString()));

            group.MapGet("/color", GetColorSettings).WithName("settings-get-color");

            return group;
        }

        public static async Task<IResult> UpdateSettings(ISender sender, [FromBody]SettingsDto request, int id,CancellationToken cancellationToken)
        {
            var result = await sender.Send(new UpdateSettingsCommand(request,id), cancellationToken);
            return TypedResults.Ok(result);
        }
        
        public static async Task<IResult> GetColorSettings(ISender sender, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new GetColorSettingsQuery(), cancellationToken);
            return TypedResults.Ok(result);
        }
    }
}

