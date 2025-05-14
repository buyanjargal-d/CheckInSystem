using Microsoft.EntityFrameworkCore;
using CheckInSystem.Data.Models;

namespace CheckInSystem.Data;

public class CheckInDbContext : DbContext
{
    public CheckInDbContext(DbContextOptions<CheckInDbContext> options) : base(options) {}

    public DbSet<Flight> Flights => Set<Flight>();
    public DbSet<Seat> Seats => Set<Seat>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Seed initial data
        modelBuilder.Entity<Flight>().HasData(
            new Flight
            {
                Id = 1,
                FlightNumber = "AB123",
                Status = "CheckingIn",
                DepartureTime = new DateTime(2025, 05, 15, 8, 0, 0, DateTimeKind.Utc) // ✅ static value
            }
        );


        modelBuilder.Entity<Seat>().HasData(
            new Seat { SeatId = 1, SeatNumber = "1A", IsOccupied = false, FlightId = 1 },
            new Seat { SeatId = 2, SeatNumber = "1B", IsOccupied = false, FlightId = 1 }
        );
    }
}
