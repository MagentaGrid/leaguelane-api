using Leaguelane.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Leaguelane.Persistence.Context;

public class LeaguelaneDbContext : DbContext
{
    public LeaguelaneDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Room> Rooms => Set<Room>();
    public DbSet<User> Users => Set<User>();
    public DbSet<RoomType> RoomTypes => Set<RoomType>();
    public DbSet<PriceConfig> PriceConfigs => Set<PriceConfig>();
    public DbSet<Booking> Bookings => Set<Booking>();
    public DbSet<Contact> Contacts => Set<Contact>();
    public DbSet<About> Abouts => Set<About>();
    public DbSet<Settings> Settings => Set<Settings>();
    public DbSet<ImageGallery> ImageGalleries => Set<ImageGallery>();
    public DbSet<Meal> Meals => Set<Meal>();
}