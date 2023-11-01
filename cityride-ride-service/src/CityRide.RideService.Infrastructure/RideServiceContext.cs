using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using CityRide.RideService.Domain.Dtos;
using CityRide.RideService.Domain.Entities;

namespace CityRide.RideService.Infrastructure;

public class RideServiceContext : DbContext
{
    private readonly string _connectionString;

    public RideServiceContext(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("RideServiceContextDb") ?? string.Empty;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(_connectionString);
    }

    public DbSet<Ride> Rides { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Ride>(ride =>
        {
            ride.HasKey(l => l.RideId);
            ride.OwnsOne(r => r.From);
            ride.OwnsOne(r => r.To);
        });
    }
}