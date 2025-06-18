using Leaguelane.Api.Handlers;
using Leaguelane.Constants.Enums;
using Leaguelane.Models.Dtos;
using MediatR;

namespace Leaguelane.Api.Endpoints;

public static class AboutEndpoint
{
    public static RouteGroupBuilder AddAboutRoutes(this RouteGroupBuilder group)
    {
        group.MapGet("", GetAbout).WithName("about");

        group.MapPost("", CreateAbout).WithName("about-create")
            .RequireAuthorization(policy => policy.RequireRole(UserRole.Admin.ToString(), UserRole.Employee.ToString()));

        group.MapPut("", UpdateAbout).WithName("about-update")
            .RequireAuthorization(policy => policy.RequireRole(UserRole.Admin.ToString(), UserRole.Employee.ToString()));

        return group;
    }

    public static async Task<IResult> GetAbout(ISender sender, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetAboutQuery(), cancellationToken);
        return TypedResults.Ok(result);
    }

    public static async Task<IResult> CreateAbout(ISender sender, AboutDto request, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new CreateAboutCommand(request), cancellationToken);
        return TypedResults.Ok(result);
    }

    public static async Task<IResult> UpdateAbout(ISender sender, AboutDto request, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new UpdateAboutCommand(request), cancellationToken);
        return TypedResults.Ok(result);
    }
}
