using Leaguelane.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Leaguelane.Persistence.Context;

public class LeaguelaneDbContext : DbContext
{
    public LeaguelaneDbContext(DbContextOptions<LeaguelaneDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Contact> Contacts => Set<Contact>();
    public DbSet<About> Abouts => Set<About>();
    public DbSet<Sport> Sports => Set<Sport>();
    public DbSet<Season> Seasons => Set<Season>();
    public DbSet<Country> Countries => Set<Country>();
    public DbSet<League> Leagues => Set<League>();
    public DbSet<LeagueSeason> LeagueSeasons => Set<LeagueSeason>();
    public DbSet<Audit> Audits => Set<Audit>();
    public DbSet<JobConfiguration> JobConfigurations => Set<JobConfiguration>();
    public DbSet<Fixture> Fixtures => Set<Fixture>();
    public DbSet<Round> Rounds => Set<Round>();
    public DbSet<Team> Teams => Set<Team>();
    public DbSet<Venue> Venues => Set<Venue>();
    public DbSet<TeamStat> TeamStats => Set<TeamStat>();
    public DbSet<TeamStatFixture> TeamStatFixtures => Set<TeamStatFixture>();
    public DbSet<TeamStatGoal> TeamStatGoals => Set<TeamStatGoal>();
    public DbSet<Odd> Odds => Set<Odd>();
    public DbSet<OddsValue> OddsValues => Set<OddsValue>();
    public DbSet<Bookmaker> Bookmakers => Set<Bookmaker>();
    public DbSet<Bet> Bets => Set<Bet>();
    public DbSet<NotificationTemplate> NotificationTemplates => Set<NotificationTemplate>();
    public DbSet<PasswordResetToken> PasswordResetTokens => Set<PasswordResetToken>();
    public DbSet<FixturePreview> FixturePreviews => Set<FixturePreview>();
    public DbSet<FixtureTip> FixtureTips => Set<FixtureTip>();
    public DbSet<JobSchedueler> JobScheduelers => Set<JobSchedueler>();
    public DbSet<Article> Articles => Set<Article>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            var properties = entityType.GetProperties()
                .Where(p => p.ClrType == typeof(DateTime) || p.ClrType == typeof(DateTime?));

            foreach (var property in properties)
            {
                property.SetValueConverter(new Microsoft.EntityFrameworkCore.Storage.ValueConversion.ValueConverter<DateTime, DateTime>(
                    v => v.ToUniversalTime(),
                    v => DateTime.SpecifyKind(v, DateTimeKind.Utc)));
            }
        }
    }
}
