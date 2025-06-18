using Leaguelane.Api.Handlers;
using Leaguelane.ApiService.Handlers;
using Leaguelane.Constants.Enums;
using Leaguelane.Models.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Leaguelane.Api.Endpoints
{
    public static class PriceConfigEndpoint
    {
        public static RouteGroupBuilder AddPriceConfigRoutes(this RouteGroupBuilder group)
        {
            group.MapGet("", GetPriceConfigs).WithName("price-configs")
                .RequireAuthorization(policy => policy.RequireRole(UserRole.Admin.ToString(), UserRole.Employee.ToString()));

            group.MapGet("{id}", GetPriceConfigDetails).WithName("price-config")
                .RequireAuthorization(policy => policy.RequireRole(UserRole.Admin.ToString(), UserRole.Employee.ToString()));

            group.MapPost("", CreatePriceConfig).WithName("price-configs-create")
                .RequireAuthorization(policy => policy.RequireRole(UserRole.Admin.ToString(), UserRole.Employee.ToString()));

            group.MapPut("{id}", UpdatePriceConfig).WithName("price-configs-update")
                .RequireAuthorization(policy => policy.RequireRole(UserRole.Admin.ToString(), UserRole.Employee.ToString()));

            return group;
        }

        public static async Task<IResult> GetPriceConfigs(ISender sender, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new GetPriceConfigQuery(), cancellationToken);
            return TypedResults.Ok(result);
        }

        public static async Task<IResult> GetPriceConfigDetails(ISender sender, int id, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new GetPriceConfigDetailsQuery(id), cancellationToken);
            return TypedResults.Ok(result);
        }

        public static async Task<IResult> CreatePriceConfig(ISender sender, [FromBody]PriceConfigDto roomType, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new CreatePriceConfigCommand(roomType), cancellationToken);
            return TypedResults.Ok(result);
        }

        public static async Task<IResult> UpdatePriceConfig(ISender sender, int id, [FromBody]PriceConfigDto roomType, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new UpdatePriceConfigCommand(roomType, id), cancellationToken);
            return TypedResults.Ok(result);
        }
    }
}

