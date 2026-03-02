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
            group.MapGet("", GetDashboard).WithName("dashboard-list").Produces<object>(200)
                .RequireAuthorization(policy => policy.RequireRole(UserRole.Admin.ToString(), UserRole.Employee.ToString()));

            return group;
        }

        /// <summary>
        /// Get dashboard.
        /// Returns dashboard statistics for admin.
        /// </summary>
        /// <param name="dashboardFeatureService"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<IResult> GetDashboard([FromServices]IDashboardFeatureService dashboardFeatureService, CancellationToken cancellationToken)
        {
            var result = await dashboardFeatureService.GetDashboard(cancellationToken);
            return TypedResults.Ok(result);
        }
    }
}
