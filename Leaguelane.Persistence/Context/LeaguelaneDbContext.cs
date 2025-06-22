using Leaguelane.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Leaguelane.Persistence.Context;

public class LeaguelaneDbContext : DbContext
{
    public LeaguelaneDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Contact> Contacts => Set<Contact>();
    public DbSet<About> Abouts => Set<About>();
    public DbSet<Sport> Sports => Set<Sport>();
}