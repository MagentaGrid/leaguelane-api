using Leaguelane.Api.Handlers;
using Leaguelane.ApiService.Feature;
using Leaguelane.Constants.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Leaguelane.ApiService.Endpoints
{
    public static class DashboardEndpoints
    {
        public static RouteGroupBuilder AddDashboardRoutes(this RouteGroupBuilder group)
        {
            group.MapGet("", GetDashboard).WithName("dashboard-list")
                .RequireAuthorization(policy => policy.RequireRole(UserRole.Admin.ToString(), UserRole.Employee.ToString()));

            return group;
        }

        public static async Task<IResult> GetDashboard([FromServices]IDashboardFeatureService dashboardFeatureService, CancellationToken cancellationToken)
        {
            var result = await dashboardFeatureService.GetDashboard(cancellationToken);
            return TypedResults.Ok(result);
        }
    }
}
