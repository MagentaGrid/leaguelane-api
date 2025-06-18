using Leaguelane.ApiService.Handlers;
using Leaguelane.Constants.Enums;
using Leaguelane.Models.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Leaguelane.ApiService.Endpoints
{
    public static class MealsEndpoint
    {
        public static RouteGroupBuilder AddMealRoutes(this RouteGroupBuilder group)
        {
            group.MapPost("", CreateMeal).WithName("meals-create")
                .DisableAntiforgery()
                .RequireAuthorization(policy => policy.RequireRole(UserRole.Admin.ToString(), UserRole.Employee.ToString()));

            group.MapGet("", GetMeals).WithName("meals-get-all");

            group.MapGet("{id}", GetMeal).WithName("meals-get-by-id")
                .RequireAuthorization(policy => policy.RequireRole(UserRole.Admin.ToString(), UserRole.Employee.ToString()));

            group.MapPut("{id}", UpdateMeal).WithName("meals-update-by-id")
                .RequireAuthorization(policy => policy.RequireRole(UserRole.Admin.ToString(), UserRole.Employee.ToString()));
            return group;
        }

        public static async Task<IResult> CreateMeal(ISender sender, [FromForm]MealRequestDto mealRequest, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new CreateMealCommand(mealRequest), cancellationToken);
            return TypedResults.Ok(result);
        }

        public static async Task<IResult> GetMeals(ISender sender, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new GetAllMealsQuery(), cancellationToken);
            return TypedResults.Ok(result);
        }

        public static async Task<IResult> GetMeal(ISender sender, int id, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new GetMealByIdQuery(id), cancellationToken);
            return TypedResults.Ok(result);
        }

        public static async Task<IResult> UpdateMeal(ISender sender, int id, MealRequestDto mealRequest, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new UpdateMealCommand(id, mealRequest), cancellationToken);
            return TypedResults.Ok(result);
        }
    }
}
