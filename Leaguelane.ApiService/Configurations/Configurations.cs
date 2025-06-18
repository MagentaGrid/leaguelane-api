using Leaguelane.Repository.Repositories;
using Leaguelane.Service.Services;

namespace Leaguelane.Api.Configurations
{
    public static class Configurations
    {
        public static void RegisterServices(this WebApplicationBuilder builder)
        {
            //Register services
            builder.Services.RegisterServices();

            //Register repositories
            builder.Services.RegisterRepositories();
        }

        private static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            //Register user services
            services.AddScoped<IUserService, UserService>();

            //Register Jwt services
            services.AddScoped<IJwtService, JwtService>();

            //Register room type services
            services.AddScoped<IRoomTypeService, RoomTypeService>();

            //Register price config services
            services.AddScoped<IPriceConfigService, PriceConfigService>();

            //Register room services
            services.AddScoped<IRoomService, RoomService>();

            //Register booking services
            services.AddScoped<IBookingService, BookingService>();

            //Contact us services
            services.AddScoped<IContactService, ContactService>();

            //About us services
            services.AddScoped<IAboutService, AboutService>();

            //Setting service

            services.AddScoped<ISettingsService, SettingsService>();

            //Register email services
            services.AddScoped<IEmailService, EmailService>();

            //Register blob services
            services.AddScoped<IBlobService, BlobService>();

            //Register image gallery services
            services.AddScoped<IImageGalleryService, ImageGalleryService>();

            //Register meal services
            services.AddScoped<IMealService, MealService>();

            return services;
        }

        private static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            //Register user repositories
            services.AddScoped<IUserRepository, UserRepository>();

            //Register room type repositories
            services.AddScoped<IRoomTypeRepository, RoomTypeRepository>();

            //Register price config repositories
            services.AddScoped<IPriceConfigRepository, PriceConfigRepository>();

            //Register room repositories
            services.AddScoped<IRoomRepository, RoomRepository>();

            //Register booking repositories
            services.AddScoped<IBookingRepository, BookingRepository>();

            //Contact us repositories
            services.AddScoped<IContactRepository, ContactRepository>();

            //About us repositories
            services.AddScoped<IAboutRepository, AboutRepository>();

            //Setting repository

            services.AddScoped<ISettingsRepository, SettingsRepository>();

            //Register image gallery repositories
            services.AddScoped<IImageGalleryRepository, ImageGalleryRepository>();

            //Register meal repositories
            services.AddScoped<IMealRepository, MealRepository>();

            return services;
        }
    }
}
