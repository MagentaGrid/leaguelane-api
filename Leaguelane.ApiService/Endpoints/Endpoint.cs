using Leaguelane.Api.Endpoints;
using System.Text;

namespace Leaguelane.ApiService.Endpoints
{
    public static class Endpoints
    {
        public static void MapEndpoints(this WebApplication app)
        {
            app.MapGroup("/rooms").AddRoomRoutes().WithTags("Room").WithName("Room");
            app.MapGroup("/users").AddUserRoutes().WithTags("User").WithName("User");
            app.MapGroup("/room-types").AddRoomTypeRoutes().WithTags("Room-Type").WithName("Room-Type");
            app.MapGroup("/price-configs").AddPriceConfigRoutes().WithTags("Price-Config").WithName("Price-Config");
            app.MapGroup("/bookings").AddBookingRoutes().WithTags("Booking").WithName("Booking");
            app.MapGroup("/contacts").AddContactRoutes().WithTags("Contact").WithName("Contact");
            app.MapGroup("/about").AddAboutRoutes().WithTags("About").WithName("About");
            app.MapGroup("/settings").AddSettingsRoutes().WithTags("Settings").WithName("Settings");
            app.MapGroup("/email").AddEmailRoutes().WithTags("Email").WithName("Email");
            app.MapGroup("/image-gallery").AddImageGalleryRoutes().WithTags("Image-Gallery").WithName("Image-Gallery");
            app.MapGroup("/meals").AddMealRoutes().WithTags("Meal").WithName("Meal");
        }
    }
}