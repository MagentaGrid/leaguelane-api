using Leaguelane.ApiService.Feature;
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

            //Register feature services
            builder.Services.RegisterFeatureServices();
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

            //Register season services
            services.AddScoped<ISeasonService, SeasonService>();

            //Register country services
            services.AddScoped<ICountryService, CountryService>();

            //Register league services
            services.AddScoped<ILeagueService, LeagueService>();

            //Register Audit services
            services.AddScoped<IAuditService, AuditService>();

            //Register job configuration services
            services.AddScoped<IJobConfigurationService, JobConfigurationService>();

            //Register Fixture services
            services.AddScoped<IFixtureService, FixtureService>();

            //Register Odds services
            services.AddScoped<IOddsService, OddsService>();

            //Register team services
            services.AddScoped<ITeamService, TeamService>();

            //Register team stats services
            services.AddScoped<ITeamStatService, TeamStatService>();

            //Register round services
            services.AddScoped<IRoundService, RoundService>();

            //Register bookmaker services
            services.AddScoped<IBookmakerService, BookmakerService>();

            //Register bet services
            services.AddScoped<IBetService, BetService>();

            //Register email service
            services.AddScoped<IEmailService, EmailService>();

            //Register password reset token service
            services.AddScoped<IPasswordResetTokenService, PasswordResetTokenService>();

            //Register job scheduler service
            services.AddScoped<IJobSchedulerService, JobSchedulerService>();

            //Register venue services
            services.AddScoped<IVenueService, VenueService>();

            //Register tip service
            services.AddScoped<ITipService, TipService>();

            //Register preview service
            services.AddScoped<IPreviewService, PreviewService>();

            //Register article service
            services.AddScoped<IArticleService, ArticleService>();

            return services;
        }

        private static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            //Register Generic repositories
            services.AddScoped<IRepository, Repository.Repositories.Repository>();


            //Register user repositories
            services.AddScoped<IUserRepository, UserRepository>();


            //Contact us repositories
            services.AddScoped<IContactRepository, ContactRepository>();

            //About us repositories
            services.AddScoped<IAboutRepository, AboutRepository>();

            //Register sport repositories
            services.AddScoped<ISportRepository, SportRepository>();

            //Register season repositories
            services.AddScoped<ISeasonRepository, SeasonRepository>();

            //Register country repositories
            services.AddScoped<ICountryRepository, CountryRepository>();

            //Register league repositories
            services.AddScoped<ILeagueRepository, LeagueRepository>();

            //Register league season repositories
            services.AddScoped<ILeagueSeasonRepository, LeagueSeasonRepository>();

            //Register Audit repositories
            services.AddScoped<IAuditRepository, AuditRepository>();

            //Register job configuration repositories
            services.AddScoped<IJobConfigurationRepository, JobConfigurationRepository>();

            //Register Fixture repositories
            services.AddScoped<IFixtureRepository, FixtureRepository>();

            //Register Odds repositories
            services.AddScoped<IOddsRepository, OddsRepository>();

            //Register team repositories
            services.AddScoped<ITeamRepository, TeamRepository>();

            //Register team stats repositories
            services.AddScoped<ITeamStatRepository, TeamStatRepository>();

            //Register venue repositories
            services.AddScoped<IVenueRepository, VenueRepository>();

            //Register round repositories
            services.AddScoped<IRoundRepository, RoundRepository>();

            //Register bookmaker repositories
            services.AddScoped<IBookmakerRepository, BookmakerRepository>();

            //Register bet repositories
            services.AddScoped<IBetRepository, BetRepository>();

            return services;
        }

        private static IServiceCollection RegisterFeatureServices(this IServiceCollection services)
        {
            //Register user feature services
            services.AddScoped<IUserFeatureService, UserFeatureService>();

            //Register dashboard feature services
            services.AddScoped<IDashboardFeatureService, DashboardFeatureService>();

            //Register fixture feature services
            services.AddScoped<IFixtureFeatureService, FixtureFeatureService>();

            //Register jobscheduler feature services
            services.AddScoped<IJobSchedulerFeatureService, JobSchedulerFeatureService>();

            //Register odds feature services
            services.AddScoped<IOddFeatureService, OddFeatureService>();

            //Register article feature services
            services.AddScoped<IArticleFeatureService, ArticleFeatureService>();

            return services;
        }
    }
}
