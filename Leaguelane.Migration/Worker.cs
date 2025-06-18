using Leaguelane.Constants.Enums;
using Leaguelane.Persistence.Context;
using Leaguelane.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Trace;
using System.Diagnostics;

namespace Leaguelane.Migration;


public class Worker(
    IServiceProvider serviceProvider,
    IHostApplicationLifetime hostApplicationLifetime) : BackgroundService
{
    public const string ActivitySourceName = "Migrations";
    private static readonly ActivitySource s_activitySource = new(ActivitySourceName);

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        using var activity = s_activitySource.StartActivity("Migrating database", ActivityKind.Client);

        try
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<LeaguelaneDbContext>();

            await RunMigrationAsync(dbContext, cancellationToken);
            await SeedDataAsync(dbContext, cancellationToken);
        }
        catch (Exception ex)
        {
            activity?.RecordException(ex);
            throw;
        }

        hostApplicationLifetime.StopApplication();
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
        await SeedRoomTypeDataAsync(dbContext, cancellationToken);
        await SeedPriceConfigDataAsync(dbContext, cancellationToken);
        await SeedRoomDataAsync(dbContext, cancellationToken);
        await SeedBookingDataAsync(dbContext, cancellationToken);
        await SeedContactDataAsync(dbContext, cancellationToken);
        await SeedSettingsDataAsync(dbContext, cancellationToken);
        await SeedAboutDataAsync(dbContext, cancellationToken);

    }
    public static async Task SeedRoomTypeDataAsync(LeaguelaneDbContext dbContext, CancellationToken cancellationToken)
    {
        var roomIds = new List<int> { 1 };
        var existingUsers = dbContext.RoomTypes.Where(r => roomIds.Contains(r.RoomTypeID)).FirstOrDefault();
        if (existingUsers == null)
        {
            RoomType roomType = new()
            {
                Name = "Standard",
                Active = true
            };

            var strategy = dbContext.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                // Seed the database
                await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
                await dbContext.RoomTypes.AddAsync(roomType, cancellationToken);
                await dbContext.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            });
        }
    }

    public static async Task SeedRoomDataAsync(LeaguelaneDbContext dbContext, CancellationToken cancellationToken)
    {
        var roomIds = new List<int> { 1 };
        var existingRoom = dbContext.Rooms.Where(r => roomIds.Contains(r.RoomId)).FirstOrDefault();
        if (existingRoom == null)
        {
            Room room1 = new()
            {
                RoomNumber = 112,
                RoomTypeId = dbContext.RoomTypes.FirstOrDefault().RoomTypeID,
                Active = true,
                Status=RoomStatus.Available,
                Created=DateTime.Today,
                RoomName = "Kitkat"
            };

            Room room2 = new()
            {
                RoomNumber = 113,
                RoomTypeId = dbContext.RoomTypes.FirstOrDefault().RoomTypeID,
                Active = true,
                Status = RoomStatus.Available,
                Created = DateTime.Today,
                RoomName = "Munch"
            };

            Room room3 = new()
            {
                RoomNumber = 114,
                RoomTypeId = dbContext.RoomTypes.FirstOrDefault().RoomTypeID,
                Active = true,
                Status = RoomStatus.Available,
                Created = DateTime.Today,
                RoomName = "Cadbury"
            };

            Room room4 = new()
            {
                RoomNumber = 115,
                RoomTypeId = dbContext.RoomTypes.FirstOrDefault().RoomTypeID,
                Active = true,
                Status = RoomStatus.Available,
                Created = DateTime.Today,
                RoomName = "Mars"
            };
            Room room5 = new()
            {
                RoomNumber = 116,
                RoomTypeId = dbContext.RoomTypes.FirstOrDefault().RoomTypeID,
                Active = true,
                Status = RoomStatus.Available,
                Created = DateTime.Today,
                RoomName = "Lindt"
            };
            Room room6 = new()
            {
                RoomNumber = 117,
                RoomTypeId = dbContext.RoomTypes.FirstOrDefault().RoomTypeID,
                Active = true,
                Status = RoomStatus.Available,
                Created = DateTime.Today,
                RoomName = "Twix"
            };

            var strategy = dbContext.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                // Seed the database
                await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
                await dbContext.Rooms.AddAsync(room1, cancellationToken);
                await dbContext.Rooms.AddAsync(room2, cancellationToken);
                await dbContext.Rooms.AddAsync(room3, cancellationToken);
                await dbContext.Rooms.AddAsync(room4, cancellationToken);
                await dbContext.Rooms.AddAsync(room5, cancellationToken);
                await dbContext.Rooms.AddAsync(room6, cancellationToken);
                await dbContext.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            });
        }
    }

    public static async Task SeedUserDataAsync(LeaguelaneDbContext dbContext, CancellationToken cancellationToken)
    {
        var userIds = new List<int> { 1 };
        var existingUsers = dbContext.Users.Where(r => userIds.Contains(r.UserId)).FirstOrDefault();
        if (existingUsers == null)
        {
            User user = new()
            {
                //UserId = 1,
                UserName = "balaLeaguelane@gmail.com",
                Password = "Leaguelane@Home!",
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



    public static async Task SeedPriceConfigDataAsync(LeaguelaneDbContext dbContext, CancellationToken cancellationToken)
    {
        var roomIds = new List<int> { 1 };
        var existingPriceConfigs = dbContext.PriceConfigs.Where(r => roomIds.Contains(r.PriceConfigId)).FirstOrDefault();
        if (existingPriceConfigs == null)
        {
            PriceConfig priceConfig = new()
            {
                //PriceConfigId = 1,
                PriceConfigName = "Standard",
                Active = true,
                PriceConfigValue = 50
            };

            var strategy = dbContext.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                // Seed the database
                await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
                await dbContext.PriceConfigs.AddAsync(priceConfig, cancellationToken);
                await dbContext.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            });
        }
    }

    public static async Task SeedBookingDataAsync(LeaguelaneDbContext dbContext, CancellationToken cancellationToken)
    {
        var bookingIds = new List<int> { 1 };
        var existingPriceConfigs = dbContext.Bookings.Where(r => bookingIds.Contains(r.BookingID)).FirstOrDefault();
        if (existingPriceConfigs == null)
        {
            Booking booking = new Booking
            {
                RoomID = dbContext.Rooms.FirstOrDefault().RoomId,
                UserID = dbContext.Users.FirstOrDefault().UserId,
                CheckInDate = DateTime.UtcNow,
                CheckOutDate = DateTime.UtcNow,
                TotalGuests = 2,
                BasePrice = 100.00m,
                ExtraCharges = 20.00m,
                TotalAmount = 120.00m,
                Status = "Confirmed"
            };


            var strategy = dbContext.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                // Seed the database
                await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
                await dbContext.Bookings.AddAsync(booking, cancellationToken);
                await dbContext.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            });
        }
    }

    public static async Task SeedContactDataAsync(LeaguelaneDbContext dbContext, CancellationToken cancellationToken)
    {
        var conatctIds = new List<int> { 1 };
        var existingContacts = dbContext.Contacts.Where(r => conatctIds.Contains(r.Id)).FirstOrDefault();
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
        var existingContacts = dbContext.Abouts.Where(r => aboutIds.Contains(r.Id)).FirstOrDefault();
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

    public static async Task SeedSettingsDataAsync(LeaguelaneDbContext dbContext, CancellationToken cancellationToken)
    {
        var settingIds = new List<int> { 1 };
        var existingSettings = dbContext.Settings.Where(r => settingIds.Contains(r.SettingsId)).FirstOrDefault();
        if (existingSettings == null)
        {
            Settings setting = new Settings
            {
                //SettingsId = 1,
                Name = SiteSettings.Color.ToString(),
                Value = "PowerBI"
            };

            var strategy = dbContext.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                // Seed the database
                await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
                await dbContext.Settings.AddAsync(setting, cancellationToken);
                await dbContext.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            });
        }
    }
}
