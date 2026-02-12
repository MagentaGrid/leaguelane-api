using Leaguelane.Constants.Enums;
using Leaguelane.Persistence.Context;
using Leaguelane.Persistence.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Leaguelane.ApiService.Extensions;

public static class MigrationExtensions
{
    private const string ActivitySourceName = "Migrations";
    private static readonly ActivitySource s_activitySource = new(ActivitySourceName);

    public static async Task EnsureDatabaseCreated(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<LeaguelaneDbContext>();
        var cancellationToken = CancellationToken.None; // Using None for startup task

        using var activity = s_activitySource.StartActivity("Migrating database", ActivityKind.Client);

        try
        {
            await RunMigrationAsync(dbContext, cancellationToken);
            await SeedDataAsync(dbContext, cancellationToken);
        }
        catch (Exception ex)
        {
            //activity?.RecordException(ex);
            throw;
        }
    }

    private static async Task RunMigrationAsync(LeaguelaneDbContext dbContext, CancellationToken cancellationToken)
    {
        var strategy = dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            // Run migration in a transaction to avoid partial migration if it fails.
            await dbContext.Database.MigrateAsync(cancellationToken);
        });
    }

    private static async Task SeedDataAsync(LeaguelaneDbContext dbContext, CancellationToken cancellationToken)
    {
        await SeedUserDataAsync(dbContext, cancellationToken);
        await SeedContactDataAsync(dbContext, cancellationToken);
        await SeedAboutDataAsync(dbContext, cancellationToken);
        await SeedSportsDataAsync(dbContext, cancellationToken);
    }
      
    public static async Task SeedUserDataAsync(LeaguelaneDbContext dbContext, CancellationToken cancellationToken)
    {
       var passwordHasher = new PasswordHasher<User>();
        var userIds = new List<int> { 1 };
        var existingUsers = await dbContext.Users.Where(r => userIds.Contains(r.UserId)).FirstOrDefaultAsync(cancellationToken);
        if (existingUsers == null)
        {
            User user = new()
            {
                //UserId = 1,
                UserName = "balaLeaguelane@gmail.com",
              Password = passwordHasher.HashPassword(null, "Leaguelane@Home!"),
                FirstName = "admin",
                LastName = "admin",
                Role = UserRole.Admin,
                Active = true
            };

            var strategy = dbContext.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                // Seed the database
                await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
                await dbContext.Users.AddAsync(user, cancellationToken);
                await dbContext.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            });
        }
    }

    
    public static async Task SeedContactDataAsync(LeaguelaneDbContext dbContext, CancellationToken cancellationToken)
    {
        var conatctIds = new List<int> { 1 };
        var existingContacts = await dbContext.Contacts.Where(r => conatctIds.Contains(r.Id)).FirstOrDefaultAsync(cancellationToken);
        if (existingContacts == null)
        {
            Contact contact = new Contact
            {
                //Id = 1,
                PhoneNumber = "+1-234-567-8901",
                Email = "contact@example.com",
                Address = "123 Main Street, New York, NY 10001",
                MapEmbedUrl = "https://maps.google.com/...",
                ImageUrl = "https://example.com/images/profile.jpg",
                Active = true,
                Created = DateTime.UtcNow
            };


            var strategy = dbContext.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                // Seed the database
                await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
                await dbContext.Contacts.AddAsync(contact, cancellationToken);
                await dbContext.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            });
        }
    }

    public static async Task SeedAboutDataAsync(LeaguelaneDbContext dbContext, CancellationToken cancellationToken)
    {
        var aboutIds = new List<int> { 1 };
        var existingContacts = await dbContext.Abouts.Where(r => aboutIds.Contains(r.Id)).FirstOrDefaultAsync(cancellationToken);
        if (existingContacts == null)
        {
            About about = new About
            {
                //Id = 1,
                Title = "About Us",
                MainContent = "This is the description of the about page.",
                HeroImageUrl = "https://example.com/images/about.jpg",
                Subtitle = "Welcome to Our Website",
                Active = true,
                Created = DateTime.UtcNow
            };


            var strategy = dbContext.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                // Seed the database
                await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
                await dbContext.Abouts.AddAsync(about, cancellationToken);
                await dbContext.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            });
        }
    }

    public static async Task SeedSportsDataAsync(LeaguelaneDbContext dbContext, CancellationToken cancellationToken)
    {
        var sportIds = new List<int> { 1 };
        var exisistingSports = await dbContext.Sports.Where(r => sportIds.Contains(r.SportId)).FirstOrDefaultAsync(cancellationToken);
        if (exisistingSports == null)
        {
            Sport sport = new()
            {
                //UserId = 1,
                Name = "Football",
                Description = "Football is a team sport played between two teams of eleven players with a spherical ball.",
                Created = DateTime.UtcNow,
                Active = true,
                ApiHost = "https://api.example.com",
                ApiUrl = "https://api.example.com/sports/1",
                ApiKey = "your_api_key"
            };

            var strategy = dbContext.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                // Seed the database
                await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
                await dbContext.Sports.AddAsync(sport, cancellationToken);
                await dbContext.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            });
        }
    }
}
