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




            //Contact us services
            services.AddScoped<IContactService, ContactService>();

            //About us services
            services.AddScoped<IAboutService, AboutService>();

            //Register sport services
            services.AddScoped<ISportService, SportService>();


            return services;
        }

        private static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            //Register user repositories
            services.AddScoped<IUserRepository, UserRepository>();

           
            //Contact us repositories
            services.AddScoped<IContactRepository, ContactRepository>();

            //About us repositories
            services.AddScoped<IAboutRepository, AboutRepository>();

            //Register sport repositories
            services.AddScoped<ISportRepository, SportRepository>();


            return services;
        }
    }
}
