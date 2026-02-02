using Leaguelane.Api.Endpoints;
using System.Text;

namespace Leaguelane.ApiService.Endpoints
{
    public static class Endpoints
    {
        public static void MapEndpoints(this WebApplication app)
        {
            app.MapGroup("/users").AddUserRoutes().WithTags("User").WithName("User");
            app.MapGroup("/contacts").AddContactRoutes().WithTags("Contact").WithName("Contact");
            app.MapGroup("/about").AddAboutRoutes().WithTags("About").WithName("About");
            app.MapGroup("/sports").AddSportRoutes().WithTags("Sport").WithName("Sport");
            app.MapGroup("/jobs").AddJobRoutes().WithTags("Job").WithName("Job");
            app.MapGroup("/job-configuration").AddJobConfigurationRoutes().WithTags("Job-Configuration");
            app.MapGroup("/fixtures").AddFixtureRoutes().WithTags("Fixture").WithName("Fixture");
        }
    }
}