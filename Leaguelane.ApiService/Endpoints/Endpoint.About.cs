using Leaguelane.Api.Handlers;
using Leaguelane.Constants.Enums;
using Leaguelane.Models.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Leaguelane.Api.Endpoints;

/// <summary>
/// Endpoint group for About-related operations.
/// Registers routes to get, create and update the About resource.
/// </summary>
public static class AboutEndpoint
{
    public static RouteGroupBuilder AddAboutRoutes(this RouteGroupBuilder group)
    {
        group.MapGet("", GetAbout).WithName("about").Produces<object>(StatusCodes.Status200OK);

        group.MapPost("", CreateAbout).WithName("about-create").Produces<object>(StatusCodes.Status200OK)
            .RequireAuthorization(policy => policy.RequireRole(UserRole.Admin.ToString(), UserRole.Employee.ToString()));

        group.MapPut("", UpdateAbout).WithName("about-update").Produces<object>(StatusCodes.Status200OK)
            .RequireAuthorization(policy => policy.RequireRole(UserRole.Admin.ToString(), UserRole.Employee.ToString()));

        return group;
    }

    /// <summary>
    /// Handles GET requests to retrieve the About information.
    /// </summary>
    /// <param name="sender">MediatR sender used to dispatch the <see cref="GetAboutQuery"/>.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>An <see cref="IResult"/> containing the About data.</returns>
    public static async Task<IResult> GetAbout(ISender sender, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetAboutQuery(), cancellationToken);
        return TypedResults.Ok(result);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <summary>
    /// Handles POST requests to create About information.
    /// </summary>
    /// <param name="sender">MediatR sender used to dispatch the <see cref="CreateAboutCommand"/>.</param>
    /// <param name="request">The <see cref="AboutDto"/> containing the data to create.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>An <see cref="IResult"/> with the creation result.</returns>
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
